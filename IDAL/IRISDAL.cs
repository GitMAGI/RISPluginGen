using System;
using System.Collections.Generic;

namespace Seminabit.Sanita.OrderEntry.RIS.IDAL
{
    public interface IRISDAL
    {
        VO.PazienteVO GetPazienteById(string pazidid);
        List<VO.PazienteVO> GetPazienteBy5IdentityFields(string pazicogn, string pazinome, string pazisess, DateTime pazidata, string pazicofi);
        int SetPaziente(VO.PazienteVO data);
        VO.PazienteVO NewPaziente(VO.PazienteVO data);
        int DeletePazienteById(string pazidid);

        List<VO.RichiestaRISVO> GetRichiesteByEpisodio(string episid);
        VO.RichiestaRISVO GetRichiestaById(string id);
        VO.RichiestaRISVO GetRichiestaByIdExt(string richidid);
        int SetRichiesta(VO.RichiestaRISVO data);
        VO.RichiestaRISVO NewRichiesta(VO.RichiestaRISVO data);
        int DeleteRichiestaById(string id);

        VO.RadioVO GetRadioById(string analidid);
        List<VO.RadioVO> GetRadiosByIds(List<string> analidids);
        List<VO.RadioVO> GetRadiosByRichiestaExt(string richidid);
        int SetRadio(VO.RadioVO data);
        List<VO.RadioVO> NewRadio(List<VO.RadioVO> data);
        VO.RadioVO NewRadio(VO.RadioVO data);
        int DeleteRadioById(string analidid);
        int DeleteRadioByIdRichiestaExt(string esamidid);
        
        string SendRISRequest(string richidid);
    }
}