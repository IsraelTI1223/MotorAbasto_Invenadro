namespace PortalGRFP.Entities.Common
{
    public class ComboGenerico
    {
        public int Id { get; set; }
        public string Valor { get; set; }
        public string IDstring { get; set; }
    }

    public class Combo3 : ComboGenerico
    {
        public int IdFiltro { get; set; }
    }

    public class Combo4 : ComboGenerico
    {
        public string Filtro { get; set; }
    }

}
