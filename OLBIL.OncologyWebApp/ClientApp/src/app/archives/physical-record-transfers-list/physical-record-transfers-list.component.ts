import { Component, OnInit } from '@angular/core';
import { PhysicalRecordTransfersClient, PhysicalRecordTransferModel } from '../../api-clients';
import { ColDef, GridOptions } from 'ag-grid-community';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';
import { GetParams } from '../../common/GetParams';
import { renderDate } from '../../common/AgGridRenderers';

@Component({
  selector: 'app-physical-record-transfers-list',
  templateUrl: './physical-record-transfers-list.component.html',
  styleUrls: ['./physical-record-transfers-list.component.css']
})
export class PhysicalRecordTransfersListComponent implements OnInit {
  isLoading: boolean = false;
  rowData: PhysicalRecordTransferModel[] = [];
  getParams: GetParams = new GetParams();

  defaultColDef: ColDef = {
    resizable: true
  };

  gridOptions: GridOptions = {};
  columnDefs: ColDef[] = [
    { headerName: 'Transferencia ID', field: 'physicalRecordTransferId' ,
      cellRendererFramework: LinkRendererComponent,
      cellRendererParams: ({ data }) => {
        return ({
          inRouterLink: `./`,
          routeParam: data.physicalRecordTransferId,
          value: data.physicalRecordTransferId
        });
      }
    },
    { headerName: 'Expendiente ID', field: 'patientPhysicalRecordId' },
    { headerName: 'Ubicación Destino', field: 'targetLocationId' },
    {
      headerName: 'Fecha', field: 'date',
      cellRenderer: ({ data }) => renderDate(data.date)
    },
    { headerName: 'Entregado por', field: 'deliveredBy' },
    { headerName: 'Recibido por', field: 'receivedBy' }
  ];

  constructor(
    private client: PhysicalRecordTransfersClient
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
