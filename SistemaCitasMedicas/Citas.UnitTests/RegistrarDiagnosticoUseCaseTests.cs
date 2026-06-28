using System;
using System.Threading.Tasks;
using Moq;
using Xunit;
using Citas.Application;
using Citas.Domain;

namespace Citas.UnitTests
{
    public class RegistrarDiagnosticoUseCaseTests
    {
        [Fact]
        public async Task EjecutarAsync_CuandoCitaNoExiste_DebeLanzarException()
        {
            //Arrange (Preparar el escenario con un Mock/Simulador del puerto)
            var mockRepository = new Mock<ICitaRepository>();

            //Simulamos que al buscar la cita con ID 99, devuelva null (no existe)
            mockRepository.Setup(repo => repo.ObtenerCitaPorIdAsync(99))
                          .ReturnsAsync((Cita?)null);

            var useCase = new RegistrarDiagnosticoUseCase(mockRepository.Object);
            var diagnosticoFalso = new Diagnostico { CitaId = 99, Descripcion = "Gripe", Tratamiento = "Reposo" };

            //Act & Assert (Actuar y Verificar que explote con la excepción correcta)
            var excepcion = await Assert.ThrowsAsync<Exception>(() => useCase.EjecutarAsync(diagnosticoFalso));
            Assert.Equal("La cita especificada no existe.", excepcion.Message);
        }
    }
}