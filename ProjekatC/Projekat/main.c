#include <stdio.h>
#include <stdlib.h>

#include "operacije.h"


int main()
{

	int running = 1;
	int userInput;

	FILE *fajl = NULL;

	while (running) {
		printf("Odaberite opciju:\n");
		printf("1 - Otvaranje datoteke\n");
		printf("2 - Formiranje datoteke\n");
		printf("3 - Pretraga datoteke\n");
        printf("4 - Unos sloga\n");
        printf("5 - Ispis svi slogova\n");
        printf("6 - Brisanje sloga (fiziko)\n");
        printf("7 - Prosecna cena lakih motocikala\n");
        printf("8 - Logicko brisanje sloga sa cenom manjom ili jednakom 0\n");
        printf("9 - Prikazi mehanicare po vrsti vozila\n");
		printf("0 - Izlaz\n");
		if (fajl == NULL) {
			printf("!!! PAZNJA: datoteka jos uvek nije otvorena !!!\n");
		}
		scanf("%d", &userInput);
		getc(stdin);

		switch(userInput) {
			case 1:
				{
					char filename[20];
					printf("Unesite ime datoteke za otvaranje: ");
					scanf("%s", &filename[0]);
					fajl = otvoriDatoteku(filename);
                    printf("\n");
					break;
				}
            case 2:
                {
                    char filename[20];
                    printf("Unesite ime datoteke za kreiranje: ");
                    scanf("%s", filename);
                    kreirajDatoteku(filename);
                    printf("\n");
                    break;
                }
            case 3:
				{
					int sifraUverenja;
					printf("Unesite sifru trazenog uverenja: ");
					scanf("%d", &sifraUverenja);
					SLOG *slog = pronadjiSlog(fajl, sifraUverenja);
					if (slog == NULL) {
                        printf("Trazeni slog ne postoji!\n");
					} else {
					    printf("Sif.Uvr   Prz.Meh      Dat      Cena       Prz.Vla   OVV\n");
                        ispisiSlog(slog);
                        printf("\n");
					}
                    printf("\n");
                break;
				}
            case 4:
				{
					SLOG evidencija;
					printf("Sifra Uverenja (broj): ");
					scanf("%d", &evidencija.sifraUverenja);
					printf("Prezime mehanicara (11 karaktera): ");
					scanf("%s", evidencija.prezimeMehanicara);
					printf("Datum (dd-mm-YYYY): ");
					scanf("%s", evidencija.datum);
					printf("Cena: ");
					scanf("%f", &evidencija.cena);
					printf("Prezime vlasnika (11 karaktera): ");
					scanf("%s", evidencija.prezimeVlasnika);
					printf("Oznaka vrste vozila (2 karaktera): ");
					scanf("%s", evidencija.oznakaVrsteVozila);
					evidencija.deleted = 0;
					dodajSlog(fajl, &evidencija);
                    printf("\n");
					break;
				}
            case 5:
				{
					ispisiSveSlogove(fajl);
                    printf("\n");
					break;
				}
            case 6:
				{
					int sifraUverenja;
					printf("Unesite sifru sloga za FIZICKO brisanje: ");
					scanf("%d", &sifraUverenja);
					obrisiSlogFizicki(fajl, sifraUverenja);
                    printf("\n");
					break;
				}
            case 7:
				{
					printf("\nProsecna cena: %.2f",prosecnaCena(fajl, "LM"));
                    printf("\n");
					break;
				}
            case 8:
				{
					obrisiLogickiPoCeni(fajl);
                    printf("\n");
					break;
				}
            case 9:
				{
					vrsteVozila(fajl);
                    printf("\n");
					break;
				}
            case 0:
				{
					//zatvori datoteku
					if (fajl != NULL) {
						fclose(fajl);
					}
					//i zavrsi program
					running = 0;
				}
            }
	}













    return 0;
}
