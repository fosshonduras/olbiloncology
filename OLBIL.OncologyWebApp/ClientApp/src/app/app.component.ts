import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit{
  year: number;
  BuildNumber: string = "0.0.0";

  ngOnInit(): void {
    this.year = new Date().getFullYear();
  }
}
