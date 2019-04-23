import { Component, OnInit } from '@angular/core';
import { AmbulatoryAttentionRecordModel, AmbulatoryAttentionRecordsClient } from '../../api-clients';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';
import { GridOptions, ColDef } from 'ag-grid-community';

@Component({
  selector: 'app-ambulatory-attention-records-list',
  templateUrl: './ambulatory-attention-records-list.component.html',
  styleUrls: ['./ambulatory-attention-records-list.component.css']
})
export class AmbulatoryAttentionRecordsListComponent implements OnInit {
  isLoading: boolean = false;
  rowData: AmbulatoryAttentionRecordModel[] = [];

  defaultColDef: ColDef = {
    resizable: true
  };

  gridOptions: GridOptions = {};
  columnDefs: ColDef[] = [
    {
      headerName: 'Registro de AT-1 ID', field: 'ambulatoryAttentionRecordId',
      cellRendererFramework: LinkRendererComponent,
      cellRendererParams: ({ data }) => {
        return ({
          inRouterLink: `./`,
          routeParam: data.ambulatoryAttentionRecordId,
          value: data.ambulatoryAttentionrecordId
        });
      }
    },
    { headerName: 'Professional de Salud ID', field: 'healthProfessionalId' },
    { headerName: 'Paciente ID', field: 'oncologyPatientId' },
    { headerName: 'Fecha', field: 'date' },
    { headerName: 'Es Nuevo?', field: 'isNewPatient' },
    { headerName: 'Dianóstico ID', field: 'diagnosisId' },
    { headerName: 'Fase de tratamiento', field: 'treatmentPhase' },
    { headerName: 'Siguiente consulta', field: 'nextAppointmentDate' },
    { headerName: 'Evento de Enfermedad', field: 'diseaseEventDescription' },
    { headerName: 'Referido a', field: 'referredTo' },
    { headerName: 'Recibido de', field: 'receivedFrom' },

  ];

  constructor(
    private client: AmbulatoryAttentionRecordsClient
  ) { }

  ngOnInit() {
    this.isLoading = true;

    this.client.getAll()
      .subscribe(result => {
        this.rowData = result.items;
        this.isLoading = false;
        this.gridOptions.columnApi.autoSizeAllColumns();
      }, err => {
        console.log(err);
      })
  }
}
