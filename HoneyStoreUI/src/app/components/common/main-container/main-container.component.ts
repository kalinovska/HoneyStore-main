import { Component, ViewChild } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthenticationService, CartItemService, WishService } from '../../../services';
import { Router, RouterOutlet } from '@angular/router';
import { MatSidenav, MatSidenavModule } from '@angular/material/sidenav';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FooterComponent } from '../footer/footer.component';
import { NavbarComponent } from '../navbar/navbar.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SidebarComponent } from '../sidebar/sidebar.component';
import { TabName } from '../../../constants/tab-name';

@Component({
  selector: 'app-main-container',
  standalone: true,
  imports: [CommonModule,
    FormsModule,
    ReactiveFormsModule,
    MatSidenavModule, 
    MatButtonModule, 
    MatIconModule,
    MatFormFieldModule,
    RouterOutlet, 
    NavbarComponent,
    SidebarComponent,
    FooterComponent,
    MatInputModule],
  templateUrl: './main-container.component.html',
  styleUrl: './main-container.component.css'
})
export class MainContainerComponent {
  @ViewChild('sidenav') sidenav: MatSidenav | null = null;

  reason: string = '';
  cartTab: TabName = TabName.Cart;
  wishTab: TabName = TabName.Wish;
  activeSidenavTab: TabName = TabName.Cart;
  textToSearch: string = '';

  constructor(public authSvc: AuthenticationService,
    public cartSvc: CartItemService,
    public wishSvc: WishService,
    public router: Router) { }

  open(tabName: TabName) {
    if(tabName === TabName.Cart) {
      this.cartSvc.getItemsByUserId(this.authSvc.currentUserValue?.id).subscribe(data=>{
        this.cartSvc.cartItemsValue = data;
      });
    } else if(tabName === TabName.Wish) {
      this.wishSvc.getWishesByUserId(this.authSvc.currentUserValue?.id).subscribe(data=>{
        this.wishSvc.wishListValue = data;
      });
    }
    this.activeSidenavTab = tabName;
    this.sidenav?.open();
  }

  close(reason: string) {
    this.reason = reason;
    this.sidenav?.close();
  }

  logout() {
    this.authSvc.logout();
    this.router.navigate(['/login']);
  }

  login() {
    this.router.navigate(['/login']);
  }

  search(value: string) {
    this.router.navigate(['/books'], { queryParams: { title: value } })
  }

  onSidebarClosed() {
    this.sidenav?.close();
  }
}
