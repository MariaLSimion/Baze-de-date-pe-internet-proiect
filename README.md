# SimionMariaBDIProiectTablou

Baza de date pentru gestiunea unei galerii de licitatii ASP .NET entity framework


Descrierea bazei de date

Tema proiectului: cuvantul cheie TABLOU
Interpretare: Baza de date pentru managementul unei galerii de licitatii 
Tabela Tablouri:
Aceasta tabela contine informatii despre lucrarile de arta disponibile pentru lictatie avand drept campuri: idLucrare, titlu, artist, anRealizare, tehnica, dimensiuni, valoare minima, disponibilitate, url pentru o poza de referinta, unde idLucrare este cheia primara a lucrarii. 
Verificari necesare pentru inserare:
-	Verificari de format (data sa nu fie din viitor, valoarea minima > 0)
Tabela Licitatii:
Acest tabel conține informații despre licitațiile organizate de galerie. Fiecare rând din acest tabel ar putea reprezenta o licitație, și  conține detalii precum id, data, ora, locația licitației, suma minima si statusul licitatie (inchisa, deschisa, va urma) unde idLicitatie reprezinta cheia primara.
Verificari necesare pentru inserare:
-	Verificari de format (suma > 0 iar statusul sa fie doar dintre cele 3 variante, un fel de enum)
Tabela LucrariLicitatii
Avand In Vedere ca intre tabela Lucrari si tabela Licitatii exista o relatie many to many am creat tabela intermediara LucrariLicitatii. Acest tabel intermediar leagă lucrările de artă de licitații. Fiecare rând din acest tabel reprezenta o asociere între o lucrare de artă și o licitație, și conține detalii precum ID-ul lucrării și ID-ul licitației (chei externe pt tabelele Lucrari si Licitatii), precum si un ID ce reprezinta cheia primara a tabelei.
Verificari necesare pentru inserare:
-	Verificare existenta lucrare in tabela Lucrari
-	Verificare existenta licitatie in tabela Licitatii

Tabela Clienti:
Acest tabel conține informații despre clienții care participă la licitații. Fiecare rând din acest tabel reprezinta un client, și conține detalii precum numele, adresa, numărul de telefon  adresa si email. 
Verificari necesare pentru inserare:
-	Verificari de format (pt email si nr de telefon)
Tabela Oferte:
Acest tabel înregistrează ofertele făcute de clienți pentru lucrările de artă în cadrul licitațiilor. Fiecare rând din acest tabel reprezinta o ofertă făcută de un client pentru o anumită lucrare de artă la o anumită licitație, avand urmatoarele campuri: IDOferta (cheie primara), IDLucrare, IDClient, IDLicitatie (chei externe pentru tabelele Lucrari, Clienti, Licitatii).  
Legaturi:
-	 tabelul LucrariArta prin intermediul ID-ului lucrării (IDLucrare). (Relatie one to many)
-	 tabelul Clienti prin intermediul ID-ului clientului (IDClient) (Relatie one to many)
Verificari necesare pentru inserare:
-	Existenta client in tabela Clienti
-	Exista Lucrare si disponibilitate lucrare
-	Existenta licitatie
Tabela CastigatoriLicitatii:
Acest tabel înregistrează informații despre câștigătorii fiecărei licitații. Fiecare rând din acest tabel reprezinta câștigătorul unei licitații și  conține detalii precum IDCastigator (cheie primara), IDLicitatie, IDLucrare, IDClient (chei externe ce leaga tabelele Licitatie, Lucrare, Client) suma câștigătoare si data castigarii.
Legaturi:
-	tabelul Tablouri prin intermediul ID-ului lucrării (IDLucrare) (Relatie one to many)
-	tabelul Licitatii prin intermediul ID-ului licitatiei (IDLicitatie) (Relatie one to many)
-	tabelul Clienti prin intermediul ID-ului clientului (IDClient) (Relatie one to many)


Idei filtrare tablouri:
-	filtare cu radio button dupa disponibilitate
-	filtrare cu checkbox dupa tehnica



Cerinte proiect 
Proiect BazeDateInternet - 50% (5 puncte din nota finala)
 1. Componenta DataBinding - Controale 3p
 a. Proiectare schema baze de date
 b. O BD relationala cu cel putin 4/5 tabele (cel putin o relatie *-*) si date valide
 c. Utilizare GridView cu legatura la o baza de date
 d. Filtrari pe baza de controale cu databinding
 e. Operatii de Select/Insert/Update/Delete din Gridview
 f. Lucrul cu obiecte de tip SqlDataSource, SqlDataAdapter, SqlDataReader
 2. Componenta de Proceduri/Functii Stocate 3p
 a. Functii stocate cu return
 b. Proceduri stocate cu parametri de IN/OUT
 c. Functii / Proceduri care returneaza un cursor
 d. Conectare rezultat functii / proceduri la un GridView
 3. Componenta de ZedGraphChart 3p
 a. Minim 2 grafice diferite (ex. Pie si Lines)
 b. Cel putin un set de date agregate pe baza tabelelor
 c. Unul din cele 2 grafice sa aiba la baza datele returnate de o procedura/functie stocata
 BONUS:-validari la nivelul controalelor ( controale de validare)-lucrul cu variabile de sesiune / cache-afisare date din mai multe tabele intr-un singur Gridview
 OBLIGATORIU:-tratarea exceptiilor in cadrul codului sursa





