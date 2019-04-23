import { Component, OnInit } from '@angular/core';
import { DiagnosisModel, CountriesClient, DiagnosesClient } from '../../api-clients';
import { ColDef, GridOptions } from 'ag-grid-community';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';

@Component({
  selector: 'app-diagnoses-list',
  templateUrl: './diagnoses-list.component.html',
  styleUrls: ['./diagnoses-list.component.css']
})
export class DiagnosesListComponent implements OnInit {
  isLoading: boolean = false;
  rowData: DiagnosisModel[] = [];

  defaultColDef: ColDef = {
    resizable: true
  };

  gridOptions: GridOptions = {};
  columnDefs: ColDef[] = [
    { headerName: 'País ID', field: 'diagnosisId' },
    {
      headerName: 'Código CIE', field: 'icdCode',
      cellRendererFramework: LinkRendererComponent,
      cellRendererParams: ({ data }) => {
        return ({
          inRouterLink: `./`,
          routeParam: data.diagnosisId,
          value: data.icdCode
        });
      }
    },
    { headerName: 'Descripción corta', field: 'shortDescriptor' },
    { headerName: 'Descripción larga', field: 'completeDescriptor' },
  ];

  constructor(
    private client: DiagnosesClient
  ) { }

  ngOnInit() {
    this.isLoading = true;

    this.client.getAll()
      .subscribe(result => {
        this.rowData = result.items;
        this.isLoading = false;
        //this.gridOptions.columnApi.autoSizeAllColumns();
      }, err => {
        console.log(err);
      })
  }
}
