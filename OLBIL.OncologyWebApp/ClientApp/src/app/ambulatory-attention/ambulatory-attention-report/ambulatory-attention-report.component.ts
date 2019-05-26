import { Component, OnInit } from '@angular/core';
import { HealthProfessionalModel, OncologyPatientModel, DiagnosisModel, AmbulatoryAttentionRecordModel, AmbulatoryAttentionRecordsClient, FilterSpec, DiagnosesClient, HealthProfessionalsClient, OncologyPatientsClient, AT1ReportItemDTO } from '../../api-clients';
import { GridOptions, ColDef, ColumnApi, GridApi } from 'ag-grid-community';
import { LinkRendererComponent } from '../../helper-components/LinkRendererComponent';
import { SearchParams } from '../../common/SearchParams';
import { Observable, of } from 'rxjs';
import { distinctUntilChanged, debounceTime, tap, switchMap, map, catchError } from 'rxjs/operators';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { renderDate, renderYesNo } from '../../common/AgGridRenderers';

@Component({
  selector: 'app-ambulatory-attention-report',
  templateUrl: './ambulatory-attention-report.component.html',
  styleUrls: ['./ambulatory-attention-report.component.css']
})
export class AmbulatoryAttentionReportComponent implements OnInit {
  /* Filters */
  healthProfessionalModel: HealthProfessionalModel;
  isSearchingHealthProfessional: boolean = false;
  healthProfessionalSearchFailed: boolean = false;

  oncologyPatientModel: OncologyPatientModel;
  isSearchingOncologyPatient: boolean = false;
  oncologyPatientSearchFailed: boolean = false;

  diagnosisModel: DiagnosisModel;
  isSearchingDiagnosis: boolean = false;
  diagnosisSearchFailed: boolean = false;

  ambulatoryAttentionRecordDate: string;
/* Results */
  isLoadingReport: boolean = false;
  rowData: AT1ReportItemDTO[] = [];
  searchParams: SearchParams = new SearchParams();

  defaultColDef: ColDef = {
    resizable: true
  };

  gridOptions: GridOptions = {
    defaultColDef: {
      resizable: true
    },
    suppressColumnVirtualisation: true
  };
  gridApi: GridApi;
  gridColumnApi: ColumnApi;

  columnDefs: ColDef[] = [
    {
      headerName: 'Registro de AT-1 ID', field: 'ambulatoryAttentionRecordId',
      cellRendererFramework: LinkRendererComponent,
      cellRendererParams: ({ data }) => {
        return ({
          inRouterLink: `./`,
          routeParam: data.ambulatoryAttentionRecordId,
          value: data.ambulatoryAttentionRecordId
        });
      }
    },
    {
      headerName: 'Fecha', field: 'date',
      cellRenderer: ({ data }) => renderDate(data.date)
    },
    { headerName: 'Professional de Salud', field: 'healthProfessionalFullName' },
    { headerName: 'Paciente', field: 'oncologyPatientFullName' },
    { headerName: 'Número de Historial Clínico', field: 'patientPhysicalRecordNumber' },
    { headerName: 'Número de Identidad del Paciente', field: 'oncologyPatientGovernmentIDNumber' },
    { headerName: 'Género', field: 'gender' },
    {
      headerName: 'Fecha de Nacimiento', field: 'date',
      cellRenderer: ({ data }) => renderDate(data.birthdate)
    },
    { headerName: 'Edad Años', field: 'ageInYears' },
    { headerName: 'Edad Meses', field: 'ageInMonthsOverYears' },
    { headerName: 'Edad Días', field: 'ageInDaysOverMonths' },
    {
      headerName: 'Es Nuevo?', field: 'isNewPatient',
      cellRenderer: ({data}) => renderYesNo(data.isNewPatient)
    },
    { headerName: 'Dianóstico', field: 'diagnosisName' },
    { headerName: 'Fase de tratamiento', field: 'treatmentPhase' },
    {
      headerName: 'Siguiente consulta', field: 'nextAppointmentDate',
      cellRenderer: ({ data }) => renderDate(data.nextAppointmentDate)
    },
    { headerName: 'Evento de Enfermedad', field: 'diseaseEventDescription' },
    { headerName: 'Referido a', field: 'referredTo' },
    { headerName: 'Recibido de', field: 'receivedFrom' },

  ];
  constructor(
    private route: ActivatedRoute,
    private router: Router,
    private toastr: ToastrService,
    private client: AmbulatoryAttentionRecordsClient,
    private healthProfessionalsClient: HealthProfessionalsClient,
    private oncologyPatientsClient: OncologyPatientsClient,
    private diagnosesClient: DiagnosesClient
  ) { }

  ngOnInit() {
  }

  healthProfessionalTAFormatter = (x: HealthProfessionalModel) => `${x.person.firstName} ${x.person.middleName} ${x.person.lastName}`;

  healthProfessionalSearchResultFormatter = (x: HealthProfessionalModel) => `${x.person.firstName} ${x.person.middleName} ${x.person.lastName}`;

  searchHealthProfessional = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => this.isSearchingHealthProfessional = true),
      switchMap(term =>
        this.healthProfessionalsClient.search(term)
          .pipe(
            map((bm, bmi) => bm.items),
            tap(() => this.healthProfessionalSearchFailed = false),
            catchError(() => {
              this.healthProfessionalSearchFailed = true;
              return of([]);
            }))
      ),
      tap(() => this.isSearchingHealthProfessional = false)
    )

  oncologyPatientTAFormatter = (x: OncologyPatientModel) => `${x.person.firstName} ${x.person.middleName} ${x.person.lastName}`;

  oncologyPatientSearchResultFormatter = (x: OncologyPatientModel) => `${x.person.governmentIDNumber} ${x.person.firstName} ${x.person.middleName} ${x.person.lastName}`;

  searchOncologyPatient = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => this.isSearchingOncologyPatient = true),
      switchMap(term =>
        this.oncologyPatientsClient.search(term)
          .pipe(
            map((bm, bmi) => bm.items),
            tap(() => this.oncologyPatientSearchFailed = false),
            catchError(() => {
              this.oncologyPatientSearchFailed = true;
              return of([]);
            }))
      ),
      tap(() => this.isSearchingOncologyPatient = false)
    )

  diagnosisTAFormatter = (x: DiagnosisModel) => `${x.shortDescriptor}`;

  diagnosisSearchResultFormatter = (x: DiagnosisModel) => `${x.icdCode} ${x.shortDescriptor}`;

  searchDiagnosis = (text$: Observable<string>) =>
    text$.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      tap(() => this.isSearchingDiagnosis = true),
      switchMap(term =>
        this.diagnosesClient.search(term)
          .pipe(
            map((bm, bmi) => bm.items),
            tap(() => this.diagnosisSearchFailed = false),
            catchError(() => {
              this.diagnosisSearchFailed = true;
              return of([]);
            }))
      ),
      tap(() => this.isSearchingDiagnosis = false)
    )

  loadReport() {
    this.isLoadingReport = true;
    this.searchParams.filters = this.buildFilters();
    this.client
      .getReport(
        this.searchParams.searchTerm, this.searchParams.filters,
        this.searchParams.sortInfo, this.searchParams.pageIndex, this.searchParams.pageSize
      )
      .subscribe(result => {
        this.rowData = result.items;
        this.isLoadingReport = false;
        this.searchParams.totalCount = result.totalCount;
        this.autoSizeAll();
      }, err => {
        console.log(err);
      })
  }

  autoSizeAll() {
    if (this.gridColumnApi) {
      this.gridColumnApi.autoSizeAllColumns();
    }
  }

  onPageChanged(newPage: number) {
    this.searchParams.pageIndex = newPage;
    this.loadReport();
  }

  onGridReady(params) {
    this.gridApi = params.api;
    this.gridColumnApi = params.columnApi;
  }

  firstDataRendered(params) {
    this.gridApi = params.api;
    this.gridColumnApi = params.columnApi;
    this.gridColumnApi.autoSizeAllColumns();
  }

  buildFilters(): FilterSpec[] {
    let newFilters: FilterSpec[] = [];
    if (this.healthProfessionalModel) {
      let healthProfessionalFilter = new FilterSpec({
        searchTerm: this.healthProfessionalModel.healthProfessionalId.toString(),
        column: "healthProfessionalId"
      });
     
      newFilters.push( healthProfessionalFilter );
    }

    if (this.oncologyPatientModel) {
      let oncologyPatientFilter = new FilterSpec({
        searchTerm : this.oncologyPatientModel.oncologyPatientId.toString(),
        column : "oncologyPatientId"
      });
      
      newFilters.push(oncologyPatientFilter);
    }

    if (this.diagnosisModel) {
      let diagnosisFilter = new FilterSpec({
        column: "diagnosisId",
        searchTerm: this.diagnosisModel.diagnosisId.toString()
      });
      newFilters.push(diagnosisFilter);
    }

    if (this.ambulatoryAttentionRecordDate) {
      let dateFilter = new FilterSpec({
        column: "date",
        searchTerm:this.ambulatoryAttentionRecordDate
      });
      newFilters.push(dateFilter);
    }
    return newFilters;
  }
}
