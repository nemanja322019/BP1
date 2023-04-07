#include "operacije.h"

FILE *otvoriDatoteku(char *filename) {
	FILE *fajl = fopen(filename, "rb+");
	if (fajl == NULL) {
		printf("Doslo je do greske pri otvaranju datoteke \"%s\"! Moguce da datoteka ne postoji.\n", filename);
	} else {
		printf("Datoteka \"%s\" uspesno otvorena!\n", filename);
	}
	return fajl;
}

void kreirajDatoteku(char *filename) {
	FILE *fajl = fopen(filename, "wb");
	if (fajl == NULL) {
		printf("Doslo je do greske prilikom kreiranja datoteke \"%s\"!\n", filename);
	} else {
		//Upisi pocetni blok
		BLOK blok;
		blok.slogovi[0].sifraUverenja = OZNAKA_KRAJA_DATOTEKE;
		fwrite(&blok, sizeof(BLOK), 1, fajl);
		printf("Datoteka \"%s\" uspesno kreirana.\n", filename);
		fclose(fajl);
	}
}

SLOG *pronadjiSlog(FILE *fajl, int sifraUverenja) {
	if (fajl == NULL) {
		return NULL;
	}

	fseek(fajl, 0, SEEK_SET);
	BLOK blok;

	while (fread(&blok, sizeof(BLOK), 1, fajl)) {

		for (int i = 0; i < FBLOKIRANJA; i++) {
			if (blok.slogovi[i].sifraUverenja == OZNAKA_KRAJA_DATOTEKE ||
                blok.slogovi[i].sifraUverenja > sifraUverenja) {
				//Nema vise slogova posle sloga sa oznakom kraja datoteke,
                //ili smo stigli do sloga sa vrednoscu kljuca vecom od vrednosti
                //kljuca sloga koji dodajemo u datoteku.

				return NULL;

			} else if (blok.slogovi[i].sifraUverenja == sifraUverenja) {
				//Pronadjen trazeni slog

				if (!blok.slogovi[i].deleted) {
                    SLOG *slog = (SLOG *)malloc(sizeof(SLOG));
                    memcpy(slog, &blok.slogovi[i], sizeof(SLOG));
                    return slog;
				}

			}
		}
	}

	return NULL;
}

void dodajSlog(FILE *fajl, SLOG *slog) {
	if (fajl == NULL) {
		printf("Datoteka nije otvorena!\n");
		return;
	}

	//Treba pronaci poziciju prvog sloga sa vrednoscu kljuca vecom od vrednosti
	//kljuca sloga koji upisujemo u datoteku. Na to mesto treba smestiti slog
	//koji upisujemo, a slog koji je vec bio na toj poziciji upisati u datoteku
	//na isti nacin.

	//Slog koji se trenutno upisuje ce se nalaziti u promenljivoj "slogKojiUpisujemo"
	SLOG slogKojiUpisujemo;
	memcpy(&slogKojiUpisujemo, slog, sizeof(SLOG));

	BLOK blok;
	fseek(fajl, 0, SEEK_SET);
	while (fread(&blok, sizeof(BLOK), 1, fajl)) {

		for (int i = 0; i < FBLOKIRANJA; i++) {

			if (blok.slogovi[i].sifraUverenja == OZNAKA_KRAJA_DATOTEKE) {
				//Ako smo stigli do kraja datoteke, znaci da nema slogova
				//u datoteci sa vrednoscu kljuca vecom od vrednosti kljuca
				//sloga koji upisujemo; na to mesto cemo staviti
				//slog koji se upisuje u datoteku, a oznaku kraja
				//cemo pomeriti za jedno mesto unapred.
				memcpy(&blok.slogovi[i], &slogKojiUpisujemo, sizeof(SLOG));

				//Na mesto ispred unetog, sad treba staviti slog sa oznakom kraja datoteke;
				//Kako to izvesti, zavisi od toga da li ima praznih mesta u trenutnom bloku.

				if (i != FBLOKIRANJA-1) {
					//Ako tekuci slog nije poslednji slog u bloku, ima jedno
					//prazno mesto posle njega, pa tu mozemo smestiti slog
					//sa oznakom kraja datoteke.
					blok.slogovi[i+1].sifraUverenja = OZNAKA_KRAJA_DATOTEKE;

					//To je to, mozemo vratiti blok u datoteku
					fseek(fajl, -sizeof(BLOK), SEEK_CUR);
					fwrite(&blok, sizeof(BLOK), 1, fajl);

					printf("Novi slog evidentiran u datoteci.\n");
					return;

				} else {
					//Inace, nemamo vise mesta u ovom bloku.
					//Oznaku kraja moramo prebaciti u sledeci blok
					//(koji prvo moramo napraviti).

					//Kao prvo, trenutni blok je pun, pa ga mozemo vratiti
					//nazad u datoteku.
					fseek(fajl, -sizeof(BLOK), SEEK_CUR);
					fwrite(&blok, sizeof(BLOK), 1, fajl);

					//Sada mozemo napraviti novi blok i upisati u njega slog
					//sa oznakom kraja datoteke (na prvu poziciju)
					BLOK noviBlok;
					noviBlok.slogovi[0].sifraUverenja = OZNAKA_KRAJA_DATOTEKE;
					fwrite(&noviBlok, sizeof(BLOK), 1, fajl);

					printf("Novi slog evidentiran u datoteci.\n");
					printf("(dodat novi blok)\n");
					return;

				}

			} else if (blok.slogovi[i].sifraUverenja == slogKojiUpisujemo.sifraUverenja) {

                //printf("vec postoji %s == %s \n", blok.slogovi[i].evidBroj, slogKojiUpisujemo.evidBroj);

                if (!blok.slogovi[i].deleted) {
                    printf("Slog sa tom vrednoscu kljuca vec postoji!\n");
                    return;
                } else {
                    //Imamo logicki obrisan slog sa istom vrednoscu kljuca sloga
                    //kao slog koji upisujemo u datoteku. Na to mesto treba prepisati
                    //novi slog preko tog logicki izbrisanog.
                    memcpy(&blok.slogovi[i], &slogKojiUpisujemo, sizeof(SLOG));

                    //Sad samo vratimo ceo taj blok u datoteku i to je to:
                    fseek(fajl, -sizeof(BLOK), SEEK_CUR);
					fwrite(&blok, sizeof(BLOK), 1, fajl);

					printf("Novi slog evidentiran u datoteci.\n");
					printf("(prepisan preko logicki izbrisanog)\n");
					return;
                }

            } else if (blok.slogovi[i].sifraUverenja > slogKojiUpisujemo.sifraUverenja) {
				//Pronasli smo prvi slog sa kljucem vecim od ovog
				//koji upisujemo u datoteku. Na to mesto smo planirali
				//da smestimo novi slog. Pre nego sto to uradimo, sacuvacemo
				//slog koji se vec nalazi na tom mestu, u promenljivu tmp,
				//a potom ga odatle prepisati u "slogKojiUpisujemo", jer cemo njega
				//sledeceg upisivati (na isti nacin kao i prethodni slog)
				SLOG tmp;
				memcpy(&tmp, &blok.slogovi[i], sizeof(SLOG));
				memcpy(&blok.slogovi[i], &slogKojiUpisujemo, sizeof(SLOG));
				memcpy(&slogKojiUpisujemo, &tmp, sizeof(SLOG));

				//Sada u "slogKojiUpisujemo" stoji slog koji je bio na tekucoj
				//poziciji (pre nego sto smo na nju upisali onaj koji
				//dodajemo). U narednoj iteraciji, taj slog ce se dodavati u datoteku
				//na isti nacin.

				//Takodje, ako je to bio poslednji slog u bloku, mozemo ceo taj
				//blok vratiti u datoteku.
				if (i == FBLOKIRANJA-1) {
					fseek(fajl, -sizeof(BLOK), SEEK_CUR);
					fwrite(&blok, sizeof(BLOK), 1, fajl);
					fflush(fajl);
				}
			}
		}
	}
}

void ispisiSveSlogove(FILE *fajl) {
	if (fajl == NULL) {
		printf("Datoteka nije otvorena!\n");
		return;
	}

	fseek(fajl, 0, SEEK_SET);
	BLOK blok;
	int rbBloka = 0;
	printf("BL SL Sif.Uvr   Prz.Meh      Dat       Cena      Prz.Vla   OVV\n");
	while (fread(&blok, sizeof(BLOK), 1, fajl)) {

		for (int i = 0; i < FBLOKIRANJA; i++) {
			if (blok.slogovi[i].sifraUverenja == OZNAKA_KRAJA_DATOTEKE) {
				printf("B%d S%d *\n", rbBloka, i);
                return; //citaj sledeci blok
			} else if (!blok.slogovi[i].deleted) {
                printf("B%d S%d ", rbBloka, i);
                ispisiSlog(&blok.slogovi[i]);
                printf("\n");
            }
		}

		rbBloka++;
	}
}

void ispisiSlog(SLOG *slog) {
	printf("%d     %10s    %10s %.2f %10s   %2s",
        slog->sifraUverenja,
		slog->prezimeMehanicara,
		slog->datum,
		slog->cena,
		slog->prezimeVlasnika,
		slog->oznakaVrsteVozila);
}

void obrisiSlogFizicki(FILE *fajl, int sifraUverenja) {

    SLOG *slog = pronadjiSlog(fajl, sifraUverenja);
    if (slog == NULL) {
        printf("Slog koji zelite obrisati ne postoji!\n");
        return;
    }

    //Trazimo slog sa odgovarajucom vrednoscu kljuca, i potom sve
    //slogove ispred njega povlacimo jedno mesto unazad.

    BLOK blok, naredniBlok;
    int sifraUverenjaZaBrisanje;
    sifraUverenjaZaBrisanje = sifraUverenja;

    fseek(fajl, 0, SEEK_SET);
    while (fread(&blok, 1, sizeof(BLOK), fajl)) {
        for (int i = 0; i < FBLOKIRANJA; i++) {

            if (blok.slogovi[i].sifraUverenja == OZNAKA_KRAJA_DATOTEKE) {

                if (i == 0) {
                    //Ako je oznaka kraja bila prvi slog u poslednjem bloku,
                    //posle brisanja onog sloga, ovaj poslednji blok nam
                    //vise ne treba;
                    printf("(skracujem fajl...)\n");
                    fseek(fajl, -sizeof(BLOK), SEEK_END);
					long bytesToKeep = ftell(fajl);
					ftruncate(fileno(fajl), bytesToKeep);
					fflush(fajl); //(da bi se primenile promene usled poziva truncate)
                }

                printf("Slog je fizicki obrisan.\n");
                return;
            }

            if (blok.slogovi[i].sifraUverenja == sifraUverenjaZaBrisanje) {

                //Obrisemo taj slog iz bloka tako sto sve slogove ispred njega
                //povucemo jedno mesto unazad.
                for (int j = i+1; j < FBLOKIRANJA; j++) {
                    memcpy(&(blok.slogovi[j-1]), &(blok.slogovi[j]), sizeof(SLOG));
                }

                //Jos bi hteli na poslednju poziciju u tom bloku upisati prvi
                //slog iz narednog bloka, pa cemo zato ucitati naredni blok...
                int podatakaProcitano = fread(&naredniBlok, sizeof(BLOK), 1, fajl);

                //...i pod uslovom da uopste ima jos blokova posle trenutnog...
                if (podatakaProcitano) {

                    //(ako smo nesto procitali, poziciju u fajlu treba vratiti nazad)
                    fseek(fajl, -sizeof(BLOK), SEEK_CUR);

                    //...prepisati njegov prvi slog na mesto poslednjeg sloga u trenutnom bloku.
                    memcpy(&(blok.slogovi[FBLOKIRANJA-1]), &(naredniBlok.slogovi[0]), sizeof(SLOG));

                    //U narednoj iteraciji while loopa, brisemo prvi slog iz narednog bloka.
                    sifraUverenjaZaBrisanje = naredniBlok.slogovi[0].sifraUverenja;
                }

                //Vratimo trenutni blok u fajl.
                fseek(fajl, -sizeof(BLOK), SEEK_CUR);
                fwrite(&blok, sizeof(BLOK), 1, fajl);
                fflush(fajl);

                if (!podatakaProcitano) {
                    //Ako nema vise blokova posle trentnog, mozemo prekinuti algoritam.
                    printf("Slog je fizicki obrisan.\n");
                    return;
                }

                //To je to, citaj sledeci blok
                break;
            }

        }
    }
}

float prosecnaCena(FILE *fajl, char* oznakaVrsteVozila){
    if (fajl == NULL) {
		printf("Datoteka nije otvorena!\n");
		return;
	}
	fseek(fajl, 0, SEEK_SET);
	BLOK blok;

	float prosecna = 0;
	int brojac = 0;

	while (fread(&blok, sizeof(BLOK), 1, fajl)) {

		for (int i = 0; i < FBLOKIRANJA; i++) {
            if(blok.slogovi[i].sifraUverenja == OZNAKA_KRAJA_DATOTEKE)
            {
                return prosecna/brojac;
            }
			else if(strcmp(blok.slogovi[i].oznakaVrsteVozila,oznakaVrsteVozila)==0 && blok.slogovi[i].deleted==0)
            {
                prosecna = prosecna + blok.slogovi[i].cena;
                brojac++;
            }
		}
	}
	return 0;
}

void obrisiLogickiPoCeni(FILE *fajl){
    if (fajl == NULL) {
		printf("Datoteka nije otvorena!\n");
		return;
	}

	fseek(fajl, 0, SEEK_SET);
	BLOK blok;
	int j=0;
	while (fread(&blok, sizeof(BLOK), 1, fajl)) {

		for (int i = 0; i < FBLOKIRANJA; i++) {
             if (blok.slogovi[i].cena < 1 && blok.slogovi[i].deleted == 0)
            {
				blok.slogovi[i].deleted = 1;
				fseek(fajl, -sizeof(BLOK), SEEK_CUR);
				fwrite(&blok, sizeof(BLOK), 1, fajl);
                printf("Brisanje sloga zavrseno.\n");
                j++;
			}
		}
		if(j==1)
            return;
	}
}

void vrsteVozila(FILE *fajl){
    if (fajl == NULL) {
		printf("Datoteka nije otvorena!\n");
		return;
	}
	fseek(fajl, 0, SEEK_SET);
	BLOK blok;

	char vrste[50][2];
	int x=0;

    while (fread(&blok, sizeof(BLOK), 1, fajl)) {
        for (int i = 0; i < FBLOKIRANJA; i++) {
            if (blok.slogovi[i].sifraUverenja == OZNAKA_KRAJA_DATOTEKE)
            {
                break;
            }
            else if ( blok.slogovi[i].deleted == 0)
            {
                strcpy(vrste[x],blok.slogovi[i].oznakaVrsteVozila);
                x++;
			}
        }
    }
    for(int i=0;i<x;i++)
    {
        for(int j=i+1;j<=x;j++)
        {
            if(vrste[i][0]==vrste[j][0] && vrste[i][1]==vrste[j][1])
            {
                strcpy(vrste[j],"\0");
            }
        }
    }

    printf("Vrsta         Mehanicari");
    for(int y=0;y<=x;y++)
    {
        fseek(fajl, 0, SEEK_SET);
        if(strcmp(vrste[y],"\0")!=0)
            printf("\n%c%c\t\t",vrste[y][0],vrste[y][1]);
        while (fread(&blok, sizeof(BLOK), 1, fajl)) {
            for (int i = 0; i < FBLOKIRANJA; i++) {
                if (blok.slogovi[i].sifraUverenja == OZNAKA_KRAJA_DATOTEKE)
                {
                    break;
                }
                else if ( blok.slogovi[i].deleted == 0 && vrste[y][0] == blok.slogovi[i].oznakaVrsteVozila[0] && vrste[y][1] == blok.slogovi[i].oznakaVrsteVozila[1])
                {
                    printf("%s\t",blok.slogovi[i].prezimeMehanicara);
                }
            }
        }
    }

}







