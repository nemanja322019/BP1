#ifndef SLOG_H_INCLUDED
#define SLOG_H_INCLUDED

#define FBLOKIRANJA 3
#define OZNAKA_KRAJA_DATOTEKE -1
typedef struct {
int sifraUverenja;
char prezimeMehanicara[10+1];
char datum[10+1];
float cena;
char prezimeVlasnika[10+1];
char oznakaVrsteVozila[2+1];
int deleted;
} SLOG;
typedef struct Blok {
SLOG slogovi[FBLOKIRANJA];
} BLOK;


#endif // SLOG_H_INCLUDED
