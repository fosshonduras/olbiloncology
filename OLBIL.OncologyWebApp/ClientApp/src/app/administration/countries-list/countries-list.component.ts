import { Component, OnInit } from '@angular/core';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';
import { ColDef, GridOptions } from 'ag-grid-community';
import { CountryModel, CountriesClient } from '../../api-clients';

@Component({
  selector: 'app-countries-list',
  templateUrl: './countries-list.component.html',
  styleUrls: ['./countries-list.component.css']
})
export class CountriesListComponent implements OnInit {
  isLoading: boolean = false;
  rowData: CountryModel[] = [];

  defaultColDef: ColDef = {
    resizable: true
  };

  gridOptions: GridOptions = {};
  columnDefs: ColDef[] = [
    { headerName: 'País ID', field: 'countryId' },
    {
      headerName: 'ISO2', field: 'isoCode2',
      cellRendererFramework: LinkRendererComponent,
      cellRendererParams: ({ data }) => {
        return ({
          inRouterLink: `./`,
          routeParam: data.countryId,
          value: data.isoCode2
        });
      }
    },
    { headerName: 'ISO3', field: 'isoCode3' },
    { headerName: 'Nombre (Es)', field: 'nameEs' },
    { headerName: 'Nombre (En)', field: 'nameEn' },
  ];

  constructor(
    private client: CountriesClient
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

