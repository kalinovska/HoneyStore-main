import { Component, EventEmitter, Input, Output } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTabsModule } from '@angular/material/tabs';
import { CartComponent } from '../../cart/cart/cart.component';
import { TabName } from '../../../constants/tab-name';
import { WishComponent } from '../../wish/wish/wish.component';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  imports: [CommonModule, MatTabsModule, CartComponent, WishComponent],
  templateUrl: './sidebar.component.html',
  styleUrl: './sidebar.component.css'
})
export class SidebarComponent {
  @Input() activeTab: TabName = TabName.Cart;
  @Output() sidebarClosed = new EventEmitter();

  cartTab: TabName = TabName.Cart;
  wishTab: TabName = TabName.Wish;

  onCartClosed() {
    this.sidebarClosed.emit()
  }
}
