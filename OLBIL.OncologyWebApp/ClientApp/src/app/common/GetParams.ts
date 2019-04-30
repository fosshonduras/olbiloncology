
export class GetParams {
  sortInfo: { [key: string]: boolean; }[] = [];
  pageIndex: number = 1;
  pageSize: number = 10;
  totalCount: number;
  totalPages: number;
}
