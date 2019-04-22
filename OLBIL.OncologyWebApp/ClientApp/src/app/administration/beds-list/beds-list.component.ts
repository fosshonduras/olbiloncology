import { Component, OnInit } from '@angular/core';
import { BedModel, BedsClient } from '../../api-clients';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';
import { AgGridNg2 } from 'ag-grid-angular';
import { GridOptions, ColDef } from 'ag-grid-community';

@Component({
  selector: 'app-beds-list',
  templateUrl: './beds-list.component.html',
  styleUrls: ['./beds-list.component.css']
})
export class BedsListComponent implements OnInit {
  isLoading: boolean = false;

  rowData: BedModel[] = [];

  defaultColDef: ColDef = {
    resizable: true
  };

  gridOptions: GridOptions = { };
  columnDefs: ColDef[] = [
    { headerName: 'Cama ID', field: 'bedId' },
    {
      headerName: 'Nombre', field: 'name',
      cellRendererFramework: LinkRendererComponent,
      cellRendererParams: ({ data }) => {
        return ({
          inRouterLink: `./`,
          routeParam: data.bedId,
          value: data.name
        });
      }
    },
    { headerName: 'Sala Id', field: 'wardId' },
    { headerName: 'Status', field: 'bedStatusName' },
  ];

  constructor(
    private client: BedsClient
  ) { }

  ngOnInit() {
    this.isLoading = true;
    this.client.getAll()
      .subscribe(result => {
        this.rowData = result.items;
        this.isLoading = false;
      }, err => {
        console.log(err);
      })
  }
}
