import { Component, OnInit } from '@angular/core';
import { AdministrativeDivisionModel, AdministrativeDivisionsClient } from '../../api-clients';
import { GridOptions, ColDef } from 'ag-grid-community';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';

@Component({
  selector: 'app-administrative-divisions-list',
  templateUrl: './administrative-divisions-list.component.html',
  styleUrls: ['./administrative-divisions-list.component.css']
})
export class AdministrativeDivisionsListComponent implements OnInit {
  isLoading: boolean = false;
  rowData: AdministrativeDivisionModel[] = [];

  defaultColDef: ColDef = {
    resizable: true
  };

  gridOptions: GridOptions = {};
  columnDefs: ColDef[] = [
    { headerName: 'División Administrativa ID', field: 'administrativeDivisionId' },
    {
      headerName: 'Código', field: 'code',
      cellRendererFramework: LinkRendererComponent,
      cellRendererParams: ({ data }) => {
        return ({
          inRouterLink: `./`,
          routeParam: data.administrativeDivisionId,
          value: data.code
        });
      }
    },
    { headerName: 'Nombre (Es)', field: 'name' },
    { headerName: 'Nivel', field: 'level' },
    { headerName: 'División Padre', field: 'parentId' },
  ];

  constructor(
    private client: AdministrativeDivisionsClient
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
