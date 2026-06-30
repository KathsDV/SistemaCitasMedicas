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
            //arrange (preparar el escenario con un mocks (simulador del puerto)
            var mockRepository = new Mock<ICitaRepository>();

            //simulamos que al buscar la cita con ID 99, devuelva null (no existe)
            mockRepository.Setup(repo => repo.ObtenerCitaPorIdAsync(99))
                          .ReturnsAsync((Cita?)null);

            var useCase = new RegistrarDiagnosticoUseCase(mockRepository.Object);
            var diagnosticoFalso = new Diagnostico { CitaId = 99, Descripcion = "Gripe", Tratamiento = "Reposo" };

            //act & assert (actuar y verificar que explote con la excepción correcta)
            var excepcion = await Assert.ThrowsAsync<Exception>(() => useCase.EjecutarAsync(diagnosticoFalso));
            Assert.Equal("La cita especificada no existe.", excepcion.Message);
        }
    }
}