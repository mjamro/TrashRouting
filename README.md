# TrashRouting
Demo for my talk: Clean up this mess - API Gateway &amp; Service Discovery in .NET

Technologies used:
1. API Gateway with RESTEase for querying data
2. Service Discovery with Consul for Services registration
3. Exchange commands from API Gateway to services with RabbitMQ
4. Events handling implementation with RabbitMQ (Kafka sample on "Kafka" branch)
5. Saga implementation

ToDo:
* Configure Polly for retrying and error handling
