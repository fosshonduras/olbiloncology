import { Component, OnInit } from '@angular/core';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';
import { ColDef, GridOptions } from 'ag-grid-community';
import { CountryModel, CountriesClient } from '../../api-clients';
import { GetParams } from '../../common/GetParams';

@Component({
  selector: 'app-countries-list',
  templateUrl: './countries-list.component.html',
  styleUrls: ['./countries-list.component.css']
})
export class CountriesListComponent implements OnInit {
  isLoading: boolean = false;
  rowData: CountryModel[] = [];
  getParams: GetParams = new GetParams();

  defaultColDef: ColDef = {
    resizable: true
  };

  gridOptions: GridOptions = {};
  columnDefs: ColDef[] = [
    { headerName: 'País ID', field: 'countryId' },
    { headerName: 'Nombre (Es)', field: 'nameEs',
      cellRendererFramework: LinkRendererComponent,
      cellRendererParams: ({ data }) => {
        return ({
          inRouterLink: `./`,
          routeParam: data.countryId,
          value: data.nameEs
        });
      }
    },
    { headerName: 'Nombre (En)', field: 'nameEn' },
    {
      headerName: 'ISO2', field: 'isoCode2'
    },
    { headerName: 'ISO3', field: 'isoCode3' },
  ];

  constructor(
    private client: CountriesClient
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

