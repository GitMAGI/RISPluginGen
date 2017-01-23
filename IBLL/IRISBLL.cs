using System.Collections.Generic;

namespace Seminabit.Sanita.OrderEntry.RIS.IBLL
{
    public interface IRISBLL
    {
        DTO.PazienteDTO GetPazienteById(string id);
        
        DTO.RichiestaRISDTO GetRichiestaRISById(string id);
        DTO.RichiestaRISDTO GetRichiestaRISByIdExt(string richidid);
        DTO.RichiestaRISDTO AddRichiestaRIS(DTO.RichiestaRISDTO rich);
        DTO.RichiestaRISDTO UpdateRichiestaRIS(DTO.RichiestaRISDTO rich);
        int DeleteRichiestaRISById(string id);

        List<DTO.RadioDTO> GetRadiosByRichiestaExt(string richidid);
        DTO.RadioDTO GetRadioById(string analidid);
        List<DTO.RadioDTO> GetRadiosByIds(List<string> analidids);
        DTO.RadioDTO UpdateRadio(DTO.RadioDTO data);
        DTO.RadioDTO AddRadio(DTO.RadioDTO data);
        List<DTO.RadioDTO> AddRadios(List<DTO.RadioDTO> data);
        int DeleteRadioById(string analidid);
        int DeleteRadioByIdRichiestaExt(string richidid);
        
        DTO.MirthResponseDTO ACKParser(string raw);
        string SendMirthRequest(string richidid);
        int ChangeHL7StatusAndMessageAll(string richidid, string hl7_stato, string hl7_msg = null);
        List<DTO.RadioDTO> ChangeHL7StatusAndMessageRadios(List<string> analidids, string hl7_stato, string hl7_msg = null);
        DTO.RichiestaRISDTO ChangeHL7StatusAndMessageRichiestaRIS(string richidid, string hl7_stato, string hl7_msg = null);
        bool ValidateRich(DTO.RichiestaRISDTO esam, ref string errorString);
        bool ValidateRadios(List<DTO.RadioDTO> anals, ref string errorString);

        bool StoreNewRequest(DTO.RichiestaRISDTO rich, List<DTO.RadioDTO> anals, ref string errorString);
        DTO.MirthResponseDTO SubmitNewRequest(string richid, ref string errorString);

        bool CheckIfCancelingIsAllowed(string richid, ref string errorString);

        DTO.RefertoDTO GetRefertoByRadioId(string id);
    }
}