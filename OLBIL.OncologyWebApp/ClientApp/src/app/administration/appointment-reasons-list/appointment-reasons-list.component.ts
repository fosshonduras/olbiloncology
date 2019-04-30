import { Component, OnInit } from '@angular/core';
import { AppointmentReasonModel, AppointmentReasonsClient } from '../../api-clients';
import { ColDef, GridOptions } from 'ag-grid-community';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';
import { GetParams } from '../../common/GetParams';

@Component({
  selector: 'app-appointment-reasons-list',
  templateUrl: './appointment-reasons-list.component.html',
  styleUrls: ['./appointment-reasons-list.component.css']
})
export class AppointmentReasonsListComponent implements OnInit {
  isLoading: boolean = false;
  rowData: AppointmentReasonModel[] = [];
  getParams: GetParams = new GetParams();

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
    this.retrieveData();
  }

  private retrieveData() {
    this.isLoading = true;
    this.getParams.sortInfo.push({ "shortDescriptor": true });
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
