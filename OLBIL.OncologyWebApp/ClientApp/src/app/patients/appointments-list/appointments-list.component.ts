import { Component, OnInit } from '@angular/core';
import { AppointmentModel, AppointmentsClient } from '../../api-clients';
import { GetParams } from '../../common/GetParams';
import { ColDef, GridOptions } from 'ag-grid-community';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';
import { renderDate, renderYesNo } from '../../common/AgGridRenderers';

@Component({
  selector: 'app-appointments-list',
  templateUrl: './appointments-list.component.html',
  styleUrls: ['./appointments-list.component.css']
})
export class AppointmentsListComponent implements OnInit {
  isLoading: boolean = false;
  rowData: AppointmentModel[] = [];
  getParams: GetParams = new GetParams();

  defaultColDef: ColDef = {
    resizable: true
  };

  gridOptions: GridOptions = {};
  columnDefs: ColDef[] = [
    {
      headerName: 'Cita ID', field: 'appointmentId',
      cellRendererFramework: LinkRendererComponent,
      cellRendererParams: ({ data }) => {
        return ({
          inRouterLink: `./`,
          routeParam: data.appointmentId,
          value: data.appointmentId
        });
      }
    },
    { headerName: 'Professional de Salud ID', field: 'healthProfessionalId' },
    { headerName: 'Paciente ID', field: 'oncologyPatientId' },
    {
      headerName: 'Fecha', field: 'date',
      cellRenderer: ({ data }) => renderDate(data.date)
    },
    { headerName: 'Estado', field: 'appointementStatus' },
    {
      headerName: 'Se presentó?', field: 'patientAttended',
      cellRenderer: ({ data }) => renderYesNo(data.patientAttended)
    },
    { headerName: 'Fase de tratamiento', field: 'treatmentPhase' },
    { headerName: 'Motivo de Cita', field: 'appointmentReason' },
    { headerName: 'Notas', field: 'notes' },
    { headerName: 'Notas Especiales', field: 'specialNotes' },
    { headerName: 'Cita Reprogramada', field: 'rescheduledAppointmentId' },
  ];

  constructor(
    private client: AppointmentsClient
  ) { }

  ngOnInit() {
    this.retrieveData();
  }

  private retrieveData() {
    this.isLoading = true; this.getParams.sortInfo.push({ "shortDescriptor": true });
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
