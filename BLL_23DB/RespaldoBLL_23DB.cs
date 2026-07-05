using DAL_23DB;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL_23DB
{
    public class RespaldoBLL_23DB
    {
        private mapperRespaldo_23DB mapperRespaldo_23DB = new mapperRespaldo_23DB();

        public string GenerarBackup_23DB(string rutaDestino_23DB)
        {
            string nombreArchivo_23DB = $"INGSW_23DB_{DateTime.Now:yyyyMMdd_HHmmss}.bak";
            string rutaCompleta_23DB = Path.Combine(rutaDestino_23DB, nombreArchivo_23DB);
            mapperRespaldo_23DB.GenerarBackup_23DB(rutaCompleta_23DB);
            return rutaCompleta_23DB;
        }

        public void RestaurarBackup_23DB(string rutaArchivo_23DB)
        {
            mapperRespaldo_23DB.RestaurarBackup_23DB(rutaArchivo_23DB);
        }
    }
}
