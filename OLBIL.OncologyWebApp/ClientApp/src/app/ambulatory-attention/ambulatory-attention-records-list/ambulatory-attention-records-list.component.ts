import { Component, OnInit } from '@angular/core';
import { AmbulatoryAttentionRecordModel, AmbulatoryAttentionRecordsClient } from '../../api-clients';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';
import { GridOptions, ColDef } from 'ag-grid-community';
import { GetParams } from '../../common/GetParams';
import { renderYesNo, renderDate } from '../../common/AgGridRenderers';

@Component({
  selector: 'app-ambulatory-attention-records-list',
  templateUrl: './ambulatory-attention-records-list.component.html',
  styleUrls: ['./ambulatory-attention-records-list.component.css']
})
export class AmbulatoryAttentionRecordsListComponent implements OnInit {
  isLoading: boolean = false;
  rowData: AmbulatoryAttentionRecordModel[] = [];
  getParams: GetParams = new GetParams();

  defaultColDef: ColDef = {
    resizable: true
  };

  gridOptions: GridOptions = {};
  columnDefs: ColDef[] = [
    {
      headerName: 'Registro de AT-1 ID', field: 'ambulatoryAttentionRecordId'
    },
    { headerName: 'Professional de Salud', field: 'healthProfessionalFullName' },
    {
      headerName: 'Fecha', field: 'date',
      cellRenderer: ({ data }) => renderDate(data.date)
    },
    { headerName: 'Paciente', field: 'oncologyPatientFullName',
      cellRendererFramework: LinkRendererComponent,
      cellRendererParams: ({ data }) => {
        return ({
          inRouterLink: `./`,
          routeParam: data.ambulatoryAttentionRecordId,
          value: data.oncologyPatientFullName
        });
      }
    },
    {
      headerName: 'Es Nuevo?', field: 'isNewPatient',
      cellRenderer: ({ data }) => renderYesNo(data.isNewPatient)
    },
    { headerName: 'Dianóstico', field: 'diagnosisName' },
    { headerName: 'Fase de tratamiento', field: 'treatmentPhase' },
    {
      headerName: 'Siguiente consulta', field: 'nextAppointmentDate',
      cellRenderer: ({ data }) => renderDate(data.nextAppointmentDate)
    },
    { headerName: 'Evento de Enfermedad', field: 'diseaseEventDescription' },
    { headerName: 'Referido a', field: 'referredTo' },
    { headerName: 'Recibido de', field: 'receivedFrom' },

  ];

  constructor(
    private client: AmbulatoryAttentionRecordsClient
  ) { }

  ngOnInit() {
    this.retrieveData();
  }

  private retrieveData() {
    this.isLoading = true;    this.getParams.sortInfo.push({ "shortDescriptor": true });
    this.client.getAll(this.getParams.sortInfo, this.getParams.pageIndex, this.getParams.pageSize)
      .subscribe(result => {
        this.rowData = result.items;
        this.isLoading = false;
        this.getParams.totalCount = result.totalCount;
      }, err => {
        console.log(err);
      })
  }

  onPageChanged(newPage: number) {
    this.getParams.pageIndex = newPage;
    this.retrieveData();
  }
}
