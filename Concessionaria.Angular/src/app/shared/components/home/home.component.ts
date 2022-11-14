import { Component, EventEmitter, Output } from '@angular/core';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent{

  @Output() public sidenavToggle = new EventEmitter();

  constructor() { }

  onToggleSidenav = () => {
    this.sidenavToggle.emit();
  }

}
