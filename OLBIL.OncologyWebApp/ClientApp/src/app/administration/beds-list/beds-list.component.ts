import { Component, OnInit } from '@angular/core';
import { BedModel, BedsClient } from '../../api-clients';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';

@Component({
  selector: 'app-beds-list',
  templateUrl: './beds-list.component.html',
  styleUrls: ['./beds-list.component.css']
})
export class BedsListComponent implements OnInit {

  rowData: BedModel[] = [];

  columnDefs = [
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
    this.client.getAll()
      .subscribe(result => {
        this.rowData = result.items;
      }, err => {
        console.log(err);
      })
  }
}
