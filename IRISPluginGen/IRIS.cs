using Seminabit.Sanita.OrderEntry.RIS.IBLL.DTO;
using System.Collections.Generic;

namespace Seminabit.Sanita.OrderEntry.RIS.IPlugin
{
    public interface IRIS
    {
        MirthResponseDTO NewRequest(RichiestaRISDTO rich, List<RadioDTO> radios, ref string errorString);

        List<RadioDTO> GetRadiosByRichIdExt(string richid);
        List<RefertoDTO> GetReportByRichIdExt(string richid_);
        RichiestaRISDTO GetRichiestaByIdExt(string richid);
        List<RichiestaRISDTO> GetRichiesteByEpisodio(string episid);

        MirthResponseDTO CancelRequest(string richid, ref string errorString);
        bool CheckIfCancelingIsAllowed(string richid, ref string errorString);

    }
}