namespace GoldenRaspberryAwards
{
    using GoldenRaspberryAwards.Data;
    using GoldenRaspberryAwards.Services;

    public partial class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();
            builder.Services.AddDbContext<ApiDbContext>();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            app.MapControllers();

            // Chama o serviço para importar os filmes do arquivo CSV e inserir no banco de dados
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                ApiDbContext context = services.GetRequiredService<ApiDbContext>();
                CsvService csvService = new CsvService(context);

                // Obter caminho do arquivo - Usar um caminho diferente quando for a execução dos testes
                string filePath = app.Configuration.GetValue<string>("TEST_ENVIRONMENT") == "true" ?
                    Path.Combine("..\\..\\..\\Data", "movielist.csv") :
                    Path.Combine("C:\\temp", "movielist.csv");

                // Importar os registros do arquivo CSV
                csvService.ImportMoviesFromCsv(filePath);
            }

            app.Run();
        }
    }
}