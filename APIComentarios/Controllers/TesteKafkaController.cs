using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Confluent.Kafka;
using APIComentarios.Models;

namespace APIComentarios.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TesteKafkaController : ControllerBase
    {
        private const string TOPIC_TESTE = "teste-kafka-aspnetcore";
        private readonly ILogger<TesteKafkaController> _logger;
        private readonly string _serverKafka;

        public TesteKafkaController(
            ILogger<TesteKafkaController> logger,
            IConfiguration configuration)
        {
            _logger = logger;
            _serverKafka = configuration["ServerKafka"];
        }

        [HttpPost]
        public ActionResult Post(Comentario comentario)
        {
            string comentarioJSON =
                JsonSerializer.Serialize(comentario);

            _logger.LogInformation($"BootstrapServers = {_serverKafka}");
            _logger.LogInformation($"Topic = {TOPIC_TESTE}");

            var config = new ProducerConfig
            {
                BootstrapServers = _serverKafka
            };

            using (var producer = new ProducerBuilder<Null, string>(config).Build())
            {
                var resultado = producer.ProduceAsync(TOPIC_TESTE,
                    new Message<Null, string>
                    {
                        Value = comentarioJSON
                    }).GetAwaiter().GetResult();

                _logger.LogInformation(
                    $"Mensagem: {comentarioJSON} | " +
                    $"Status: {resultado.Status.ToString()}");
            }

            return Ok();
        }
    }
}