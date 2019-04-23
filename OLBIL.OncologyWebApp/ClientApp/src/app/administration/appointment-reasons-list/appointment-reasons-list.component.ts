import { Component, OnInit } from '@angular/core';
import { AppointmentReasonModel, AppointmentReasonsClient } from '../../api-clients';
import { ColDef, GridOptions } from 'ag-grid-community';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';

@Component({
  selector: 'app-appointment-reasons-list',
  templateUrl: './appointment-reasons-list.component.html',
  styleUrls: ['./appointment-reasons-list.component.css']
})
export class AppointmentReasonsListComponent implements OnInit {
  isLoading: boolean = false;
  rowData: AppointmentReasonModel[] = [];

  defaultColDef: ColDef = {
    resizable: true
  };

  gridOptions: GridOptions = {};
  columnDefs: ColDef[] = [
    { headerName: 'Sala ID', field: 'appointmentReasonId' },
    {
      headerName: 'Descripción', field: 'description',
      cellRendererFramework: LinkRendererComponent,
      cellRendererParams: ({ data }) => {
        return ({
          inRouterLink: `./`,
          routeParam: data.appointmentReasonId,
          value: data.description
        });
      }
    }
  ];

  constructor(
    private client: AppointmentReasonsClient
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
