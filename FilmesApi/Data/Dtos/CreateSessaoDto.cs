namespace FilmesApi.Data.Dtos
{
    public class CreateSessaoDto
    {
        public int FilmeId { get; set; }
        public int CinemaId { get; set; }
        public DateTime HorarioDeInicio { get; set; }
        public DateTime HorarioDeEncerramento { get; set; }
    }
}
