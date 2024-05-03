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

Aggiungere un altra libreria (Serices.Premium) in cui metti un altra implementazione dell'interfaccia IBookingService la quale farà qualcosa di diverso da quella base. Per esempio se prenoti ed il libro è già occupato, ne va a creare una copia in più

Il sistema deciderà se istanziare con la depency injection un servizio piuttosto che un altro in base al nome del libro per il momento, che non ha senso ma è solo per partire. Tipo se il libro richiesto è la Bibbia usa il servizio premium


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

3- Usare il token per distinguere gli utenti premium da quelli non premium
PSEUDOCODICE di quello che intendo:

services.AddTransient<BookingServiceStandard>();
services.AddTransient<BookingServicePremium>();

services.AddTransient<IBookingService>(sp =>
{
    var ctx = sp.GetRequiredService<IHttpContextAccessor>();

    var email = ctx.HttpContext!.User.FindFirst(ClaimTypes.Email)!.Value;

    var user = sp.GetRequiredService<IUserRepository>().GetByEmail(email);

    if (user != null && user.IsPremium)
    {
        return sp.GetRequiredService<BookingServicePremium>();
    }

    return sp.GetRequiredService<BookingServiceStandard>();
});

-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------
Versione 0.0.3
https://learn.microsoft.com/en-us/aspnet/core/tutorials/signalr?view=aspnetcore-7.0&tabs=visual-studio
HostedService https://learn.microsoft.com/en-us/aspnet/core/fundamentals/host/hosted-services?view=aspnetcore-7.0&tabs=visual-studio

-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------

