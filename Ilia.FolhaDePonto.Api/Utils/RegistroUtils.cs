namespace Ilia.FolhaDePonto.Api.Utils
{
    public static class RegistroUtils
    {
        public static List<string> ExtraiHorariosParaLista(string horarios)
        {
            List<string> horariosEmLista = new List<string>();

            foreach (var horario in horarios.Split(";"))
                horariosEmLista.Add(horario);

            horariosEmLista = horariosEmLista.OrderBy(x => x).ToList();

            return horariosEmLista;
        }        
       
        public static string OrdernaHorariosParaString(List<string> horarios)
        {
            string horariosEmString = "";

            for (int i = 0; i < horarios.Count; i++)
            {
                if (i > 0)
                {
                    horariosEmString += ";" + horarios[i];
                }
                else
                {
                    horariosEmString = horarios[i];
                }
            }

            return horariosEmString;
        }
        
        public static string ExtraiHorariosParaString(string horarios)
        {
            var horariosEmLista = horarios.ToString().Split(";").ToList().OrderBy(x => x).ToList();

            return OrdernaHorariosParaString(horariosEmLista);
        }
    }
}
