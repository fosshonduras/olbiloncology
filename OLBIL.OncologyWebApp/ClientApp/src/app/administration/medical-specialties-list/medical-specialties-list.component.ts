import { Component, OnInit } from '@angular/core';
import { GridOptions, ColDef } from 'ag-grid-community';
import { MedicalSpecialtyModel, MedicalSpecialtiesClient } from '../../api-clients';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';
import { GetParams } from '../../common/GetParams';

@Component({
  selector: 'app-medical-specialties-list',
  templateUrl: './medical-specialties-list.component.html',
  styleUrls: ['./medical-specialties-list.component.css']
})
export class MedicalSpecialtiesListComponent implements OnInit {
  isLoading: boolean = false;
  rowData: MedicalSpecialtyModel[] = [];
  getParams: GetParams = new GetParams();

  defaultColDef: ColDef = {
    resizable: true
  };

  gridOptions: GridOptions = {};
  columnDefs: ColDef[] = [
    { headerName: 'Especialidad Médica ID', field: 'medicalSpecialtyId' },
    {
      headerName: 'Descripción', field: 'description',
      cellRendererFramework: LinkRendererComponent,
      cellRendererParams: ({ data }) => {
        return ({
          inRouterLink: `./`,
          routeParam: data.medicalSpecialtyId,
          value: data.description
        });
      }
    }
  ];

  constructor(
    private client: MedicalSpecialtiesClient
  ) { }

  ngOnInit() {
    this.retrieveData();
  }

  private retrieveData() {
    this.isLoading = true;    this.getParams.sortInfo.push({ "shortDescriptor": true });
    this.client.getAll(this.getParams.sortInfo, this.getParams.pageIndex, this.getParams.pageSize)
      .subscribe(result => {
        this.rowData = result.items;
        this.isLoading = false;
        this.getParams.totalCount = result.totalCount;
      }, err => {
        console.log(err);
      })
  }
}
