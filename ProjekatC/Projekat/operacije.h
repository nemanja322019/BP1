#ifndef OPERACIJE_H_INCLUDED
#define OPERACIJE_H_INCLUDED

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <unistd.h>
#include <sys/types.h>

#include "slog.h"

FILE *otvoriDatoteku(char *filename);
SLOG *pronadjiSlog(FILE *fajl, int sifraUverenja);
void dodajSlog(FILE *fajl, SLOG *slog);
void ispisiSveSlogove(FILE *fajl);
void obrisiSlogFizicki(FILE *fajl, int sifraUverenja);

void kreirajDatoteku(char *filename);
void obrisiSlogLogicki(FILE *fajl, int sifraUverenja);
float prosecnaCena(FILE *fajl, char* oznakaVrsteVozila);
void obrisiLogickiPoCeni(FILE *fajl);
void vrsteVozila(FILE *fajl);

#endif // OPERACIJE_H_INCLUDED
