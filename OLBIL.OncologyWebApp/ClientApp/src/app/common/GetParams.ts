import { SortSpec } from "../api-clients";

export class GetParams {
  sortInfo: SortSpec[] = [];
  pageIndex: number = 1;
  pageSize: number = 10;
  totalCount: number;
  totalPages: number;
}
