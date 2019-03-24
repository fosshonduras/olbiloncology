import { Component, OnInit } from '@angular/core';
import { UnitModel, UnitsClient } from '../../api-clients';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';

@Component({
  selector: 'app-units-list',
  templateUrl: './units-list.component.html',
  styleUrls: ['./units-list.component.css']
})
export class UnitsListComponent implements OnInit {
  rowData: UnitModel[] = [];

  columnDefs = [
    { headerName: 'Unit ID', field: 'unitId' },
    {
      headerName: 'Código', field: 'code',
      cellRendererFramework: LinkRendererComponent,
      cellRendererParams: ({ data }) => {
        return ({
          inRouterLink: `./`,
          routeParam: data.unitId,
          value: data.code
        });
      }
    },
    { headerName: 'Nombre', field: 'name' },
  ];

  constructor(
    private client: UnitsClient
  ) { }

  ngOnInit() {
    this.client.getAll()
      .subscribe(result => {
        this.rowData = result.items;
      }, err => {
        console.log(err);
      })
  }
}
