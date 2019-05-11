import { Component, OnInit } from '@angular/core';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';
import { GetParams } from '../../common/GetParams';
import { PatientPhysicalRecordModel, PatientPhysicalRecordsClient } from '../../api-clients';
import { GridOptions, ColDef } from 'ag-grid-community';

@Component({
  selector: 'app-patient-physical-records-list',
  templateUrl: './patient-physical-records-list.component.html',
  styleUrls: ['./patient-physical-records-list.component.css']
})
export class PatientPhysicalRecordsListComponent implements OnInit {
  isLoading: boolean = false;
  rowData: PatientPhysicalRecordModel[] = [];
  getParams: GetParams = new GetParams();

  defaultColDef: ColDef = {
    resizable: true
  };

  gridOptions: GridOptions = {};
  columnDefs: ColDef[] = [
    { headerName: 'Expediente ID', field: 'patientPhysicalRecordId' },
    { headerName: 'Número de Expediente', field: 'recordNumber',
      cellRendererFramework: LinkRendererComponent,
      cellRendererParams: ({ data }) => {
        return ({
          inRouterLink: `./`,
          routeParam: data.patientPhysicalRecordId,
          value: data.recordNumber
        });
      }
    },
    { headerName: 'Paciente Id', field: 'oncologyPatientId' },
    { headerName: 'Ubicación ID', field: 'recordStorageLocationId' },
  ];

  constructor(
    private client: PatientPhysicalRecordsClient
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
