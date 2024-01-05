import {Component, EventEmitter, OnInit, Output} from '@angular/core';

@Component({
  selector: 'app-search',
  template: ' <input type="text" placeholder="Search.." [(ngModel)]="enteredSearchValue" (input)="onSearchTextChanged()">',
                                                        //Er en tovejs databinding til både enterSearchValue og også til onSeacrchtextChanged
})
export class SearchComponent  //implements OnInit {
{



  enteredSearchValue: string = '';

@Output() //Dekoratør
searchTextChanged: EventEmitter<string> = new EventEmitter<string>(); //Måden man opsætter observer design mønsteret

  onSearchTextChanged(){
    this.searchTextChanged.emit(this.enteredSearchValue);
  }



}

