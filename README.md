-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Versione 0.0.0
Suddividere in varie librerie creando nuovi progetti: (sempre tutto all'interno della stessa solution)
- LibraryApp
- Entità			(Entities)
- Interfacce Servizi 		(Interfaces)
- Implementazioni Servizi	(Services)
- Dto				(Dtos)

Aggiungere alla solution una web API che espone 
- Ricerca per titolo
- Ricerca per autore
- Crea prenotazione 
- Cancella prenotazione
- Salva Libro
- Aggiorna Libro

Usare delle interfacce per i servizi e istanziarli tramite dependency injection.
Per servizi intendo sostanzialmente i metodi che contengono la logica dell'applicazione, per esempio prenotazione, creazione libro ecc..
Preparare degli script .sql per popolare il database con un pò di dati

-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Versione 0.0.1 
usare AutoMapper per passare dai DTO alle entità e viceversa

-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Versione 0.0.2 
Aggiungere una wep api che si occupa di fare l'autenticazione degli utenti, una sorta di login service.
- IdentityApp
L'obiettivo è quello di avere un database separato per gli utenti (le login ammesse) e tramite IdentityServer (DUENDE) fare in modo che le api progetto accettino solo richieste autenticate. 
Usare poi Postman (o programmi simili) poi per chiamare le api passandogli il token. 
-https://github.com/DuendeSoftware/IdentityServer  
Il flusso sarà:
1- fare una chiamata Api che ritorna il token ad IdentityApp
2- poi tramite Postman fare le chiamate alla LibraryApp passando nell'header il token 

Aggiungere un altra libreria (Serices.Premium) in cui metti un altra implementazione dell'interfaccia IBookingService la quale farà qualcosa di diverso da quella base. 
Per esempio se prenoti ed il libro è già occupato, ne va a creare una copia in più

Usare il token per distinguere gli utenti premium da quelli non premium

-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Versione 0.0.3

Aggiunere un'api, tipo BuyBook, in cui un utente Premium può comprare un libro che ha attualmente in prestito. 
L'api quindi riceverà come input l'identificatico del libro e sparerà una notifica. 
Una nuova web App chiamata NotificationHub recepirà la notifica e lo rigirerà tramite notifica alla Library app, 
dove ci sarà un HostedService in ascolto delle notifiche. 
Questo in base al tipo di notifica chiamerà un opportuno servizio per gestirla (nel nostro caso per esempio 
detrarrà 1 dal numero dei libri con quell'identificativo)

https://learn.microsoft.com/en-us/aspnet/core/tutorials/signalr?view=aspnetcore-7.0&tabs=visual-studio
HostedService https://learn.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-7.0&tabs=visual-studio

-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

