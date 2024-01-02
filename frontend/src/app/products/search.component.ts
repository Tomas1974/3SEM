import {Component, EventEmitter, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-search',
  template: ' <input type="text" placeholder="Search.." [(ngModel)]="enteredSearchValue" (input)="onSearchTextChanged()">',

})
export class SearchComponent  //implements OnInit {
{

//  constructor() { }

 // ngOnInit() {}

  enteredSearchValue: string = '';

@Output()
searchTextChanged: EventEmitter<string> = new EventEmitter<string>();

  onSearchTextChanged(){
    this.searchTextChanged.emit(this.enteredSearchValue);
  }



}

