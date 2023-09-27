1.Ejecuta el siguente comando docker:

docker run -d --name rabbitmq-container -p 5672:5672 -p 15672:15672 rabbitmq:management

1.Ejecuta los proyectos ProducerMessage y ServerSignal en visual Studio para desplegarlos.

2.El proyecto ProducerMessage es una web api donde contiene endpoints para crear mensajes en las colas, el proyecto tienes 4 endpoints, si deseas producir un mensaje para un usuario en especificio utiliza los endpoints que como nombre tiene User al final y mandas el id del User al endpoint, verifica en el cliente userSSE.html que el idUser sea el mismo.

3.Para levantar el cliente  abre una terminal donde tengas ubicado este proyecto y ejecuta los siguentes comandos en la terminal.

cd ClienteSSE
npm install -g live-server
live-server --port=64332

Te abrira el cliente en el navegador. Empieza producir mensajes ejecutanto los endpoints del proyecto ProducerMessage.

El cliente index.html se suscribe a un exchange en especifico y recibe todos los mensajes de ese exchange
El cliente userSSE.html se suscribe a un exchange y solo recibe los mensajes que son destinados para el user.

Objeto json que llega:

{
    "Event_id":15,
    "tracking":"65923a86-5d05-455f-9bb9-5eaa2ca804ba",
    "From":"ProduccerMessage",
    "To":null,
    "EventType":"company_new",
    "DateCreate":"2023-09-26T06:58:46.6227291-05:00",
    "Data":{
        "company_id":"de93bd20-6c3f-4be1-bc3e-72ec5222091a"
        }
}


Documentacion de signalR:

https://learn.microsoft.com/es-es/aspnet/core/signalr/javascript-client?view=aspnetcore-7.0&tabs=visual-studio
https://learn.microsoft.com/es-es/aspnet/core/signalr/dotnet-client?view=aspnetcore-7.0&tabs=visual-studio
https://learn.microsoft.com/es-es/aspnet/signalr/overview/getting-started/introduction-to-signalr