import { Component, OnInit } from '@angular/core';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';
import { WardsClient, WardModel } from '../../api-clients';

@Component({
  selector: 'app-wards-list',
  templateUrl: './wards-list.component.html',
  styleUrls: ['./wards-list.component.css']
})
export class WardsListComponent implements OnInit {
  isLoading: boolean = false;
  rowData: WardModel[] = [];

  columnDefs = [
    { headerName: 'Sala ID', field: 'wardId' },
    {
      headerName: 'Nombre', field: 'name',
      cellRendererFramework: LinkRendererComponent,
      cellRendererParams: ({ data }) => {
        return ({
          inRouterLink: `./`,
          routeParam: data.wardId,
          value: data.name
        });
      }
    },
    { headerName: 'Unidad', field: 'unitId' },
    { headerName: 'Edificio', field: 'buildingId' },
    { headerName: 'Nivel', field: 'floorNumber' },
    { headerName: 'Género', field: 'wardGenderId' }
  ];

  constructor(
    private client: WardsClient
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
