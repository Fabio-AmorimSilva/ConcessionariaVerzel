/* eslint-disable @angular-eslint/use-lifecycle-interface */
import { NavItem } from './classes/nav-item';
import { Router } from '@angular/router';
import { Component, OnInit, Renderer2 } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.scss']
})
export class NavbarComponent implements OnInit{

  items: NavItem[] = [];
  isLogged!: boolean;

  constructor(
    private authService: AuthService,
    private router: Router,) { }

  ngOnInit(): void {
    this.setItems();
    this.verifyToken();
  }

  setItems(): void{
    this.items = [
      { name: 'Home', url: '/home'},
      { name: 'Carros', url: '/home/carros'},
    ];
  }

  logout(): void{
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  verifyToken(): void{
    let token = this.authService.getToken();
    if(token == null){
      this.isLogged = false;
      this.items.push({name: 'Login', url: '/login'});
    }else{
      this.isLogged = true;
    }
  }
}
