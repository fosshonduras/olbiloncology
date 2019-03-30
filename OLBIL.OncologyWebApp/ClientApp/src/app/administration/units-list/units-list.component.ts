import { Component, OnInit } from '@angular/core';
import { HospitalUnitModel, HospitalUnitsClient } from '../../api-clients';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';

@Component({
  selector: 'app-units-list',
  templateUrl: './units-list.component.html',
  styleUrls: ['./units-list.component.css']
})
export class UnitsListComponent implements OnInit {
  isLoading: boolean = false;
  rowData: HospitalUnitModel[] = [];

  columnDefs = [
    { headerName: 'Unit ID', field: 'hospitalUnitId' },
    {
      headerName: 'Código', field: 'code',
      cellRendererFramework: LinkRendererComponent,
      cellRendererParams: ({ data }) => {
        return ({
          inRouterLink: `./`,
          routeParam: data.hospitalUnitId,
          value: data.code
        });
      }
    },
    { headerName: 'Nombre', field: 'name' },
  ];

  constructor(
    private client: HospitalUnitsClient
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
