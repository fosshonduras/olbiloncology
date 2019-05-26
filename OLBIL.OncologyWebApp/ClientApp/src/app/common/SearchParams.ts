import { GetParams } from "./GetParams";
import { FilterSpec } from "../api-clients";

export class SearchParams extends GetParams {
  searchTerm: string;
  filters: FilterSpec[] = [];
}
