using Microsoft.AspNetCore.Mvc;
using OLBIL.OncologyApplication.Models;
using OLBIL.OncologyApplication.AmbulatoryAttentionRecords.Commands;
using OLBIL.OncologyApplication.AmbulatoryAttentionRecords.Queries;
using System.Threading.Tasks;
using OLBIL.OncologyApplication.DTOs;
using System.Collections.Generic;
using OLBIL.OncologyApplication.Infrastructure;

namespace OLBIL.OncologyWebApp.Controllers
{
    public class AmbulatoryAttentionRecordsController: OlbilController
    {
        [HttpGet]
        public async Task<ActionResult<ListModel<AmbulatoryAttentionRecordModel>>> GetAll([FromQuery]GetAmbulatoryAttentionRecordsListQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpGet("search")]
        public async Task<ActionResult<ListModel<AmbulatoryAttentionRecordModel>>> Search(string searchTerm)
        {
            return Ok(await Mediator.Send(new SearchAmbulatoryAttentionRecordsQuery { SearchTerm = searchTerm }));
        }

        [HttpGet("at1-report")]
        public async Task<ActionResult<ListModel<AT1ReportItemDTO>>> GetReport([FromQuery] GetAT1ReportQuery request)
        {
            return Ok(await Mediator.Send(request));
        }

        [HttpGet("at1-report/download", Name = "GetAmbulatoryAttentionReportFile")]
        public async Task<ActionResult> GetAmbulatoryAttentionReportFile([FromQuery] GetAT1ReportQuery request)
        {
            var report = await Mediator.Send(request);
            var columnInfoList = new List<ExcelColumnInfo<AT1ReportItemDTO>>() {
                new ExcelColumnInfo<AT1ReportItemDTO>{ Order = 10, Header = nameof(AT1ReportItemDTO.Date), Accessor = e => e.Date  },
                new ExcelColumnInfo<AT1ReportItemDTO>{ Order = 10, Header = nameof(AT1ReportItemDTO.HealthProfessionalFullName), Accessor = e => e.HealthProfessionalFullName  },
                new ExcelColumnInfo<AT1ReportItemDTO>{ Order = 10, Header = nameof(AT1ReportItemDTO.PatientPhysicalRecordNumber), Accessor = e => e.PatientPhysicalRecordNumber  },
                new ExcelColumnInfo<AT1ReportItemDTO>{ Order = 10, Header = nameof(AT1ReportItemDTO.OncologyPatientFullName), Accessor = e => e.OncologyPatientFullName  },
                new ExcelColumnInfo<AT1ReportItemDTO>{ Order = 10, Header = nameof(AT1ReportItemDTO.OncologyPatientGovernmentIDNumber), Accessor = e => e.OncologyPatientGovernmentIDNumber  },
                new ExcelColumnInfo<AT1ReportItemDTO>{ Order = 10, Header = nameof(AT1ReportItemDTO.Gender), Accessor = e => e.Gender  },
                new ExcelColumnInfo<AT1ReportItemDTO>{ Order = 10, Header = nameof(AT1ReportItemDTO.Birthdate), Accessor = e => e.Birthdate == null ? string.Empty : e.Birthdate.Value.ToShortDateString()  },
                new ExcelColumnInfo<AT1ReportItemDTO>{ Order = 10, Header = nameof(AT1ReportItemDTO.AgeInYears), Accessor = e => e.AgeInYears  },
                new ExcelColumnInfo<AT1ReportItemDTO>{ Order = 10, Header = nameof(AT1ReportItemDTO.AgeInMonthsOverYears), Accessor = e => e.AgeInMonthsOverYears  },
                new ExcelColumnInfo<AT1ReportItemDTO>{ Order = 10, Header = nameof(AT1ReportItemDTO.AgeInDaysOverMonths), Accessor = e => e.AgeInDaysOverMonths },
                new ExcelColumnInfo<AT1ReportItemDTO>{ Order = 10, Header = nameof(AT1ReportItemDTO.IsNewPatient), Accessor = e => e.IsNewPatient ? "Sí" : "No"  },
                new ExcelColumnInfo<AT1ReportItemDTO>{ Order = 10, Header = nameof(AT1ReportItemDTO.ProcedenceDivisionLevel2), Accessor = e => e.ProcedenceDivisionLevel2  },
                new ExcelColumnInfo<AT1ReportItemDTO>{ Order = 10, Header = nameof(AT1ReportItemDTO.ProcedenceDivisionLevel3), Accessor = e => e.ProcedenceDivisionLevel3  },
                new ExcelColumnInfo<AT1ReportItemDTO>{ Order = 10, Header = nameof(AT1ReportItemDTO.ProcedenceDivisionLevel4), Accessor = e => e.ProcedenceDivisionLevel4  },

                new ExcelColumnInfo<AT1ReportItemDTO>{ Order = 10, Header = nameof(AT1ReportItemDTO.DiagnosisName), Accessor = e => e.DiagnosisName  },
                new ExcelColumnInfo<AT1ReportItemDTO>{ Order = 10, Header = nameof(AT1ReportItemDTO.DiseaseEventDescription), Accessor = e => e.DiseaseEventDescription  },
                new ExcelColumnInfo<AT1ReportItemDTO>{ Order = 10, Header = nameof(AT1ReportItemDTO.TreatmentPhase), Accessor = e => e.TreatmentPhase  },
                new ExcelColumnInfo<AT1ReportItemDTO>{ Order = 10, Header = nameof(AT1ReportItemDTO.NextAppointmentDate), Accessor = e => e.NextAppointmentDate == null ? string.Empty:e.NextAppointmentDate.Value.ToShortDateString() },
                new ExcelColumnInfo<AT1ReportItemDTO>{ Order = 10, Header = nameof(AT1ReportItemDTO.ReferredTo), Accessor = e => e.ReferredTo  },
                new ExcelColumnInfo<AT1ReportItemDTO>{ Order = 10, Header = nameof(AT1ReportItemDTO.ReceivedFrom), Accessor = e => e.ReceivedFrom  },

            };
            var excelFile = await ExcelFileExporter.ExportForWeb("report.xlsx", columnInfoList, report.Items);
            return File(
                fileContents: excelFile,
                contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                fileDownloadName: "report.xlsx"
            );
        }

        [HttpGet("{id}", Name = "GetAmbulatoryAttentionRecord")]
        public async Task<ActionResult<AmbulatoryAttentionRecordModel>> GetAmbulatoryAttentionRecord(int id)
        {
            return Ok(await Mediator.Send(new GetAmbulatoryAttentionRecordQuery { Id = id }));
        }

        [HttpPost]
        public async Task<ActionResult<int>> CreateAmbulatoryAttentionRecord([FromBody]AmbulatoryAttentionRecordModel model)
        {
            //return Created($"{AppConstants.API_URL_PREFIX}/ambulatoryattentionrecords/", await Mediator.Send(new CreateAmbulatoryAttentionRecordCommand { Model = model }));
            return Ok(await Mediator.Send(new CreateAmbulatoryAttentionRecordCommand { Model = model }));
        }

        [HttpPut]
        public async Task<ActionResult> UpdateAmbulatoryAttentionRecord([FromBody]AmbulatoryAttentionRecordModel model)
        {
            await Mediator.Send(new UpdateAmbulatoryAttentionRecordCommand { Model = model });
            return NoContent();
        }
    }
}
