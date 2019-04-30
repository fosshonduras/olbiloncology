import { Component } from "@angular/core";
import { GetParams } from "../common/GetParams";

@Component({
  selector: 'app-base-componet'  
})
export class BaseComponent extends Component{
  isLoading: boolean = false;
  getParams: GetParams = new GetParams();
}
