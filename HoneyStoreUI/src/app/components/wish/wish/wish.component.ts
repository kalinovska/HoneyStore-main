import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { AuthenticationService, WishService } from '../../../services';
import { FileHelper } from '../../../helpers';
import { Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';

@Component({
  selector: 'app-wish',
  standalone: true,
  imports: [CommonModule,
    MatDividerModule,
    MatCardModule,
    MatIconModule,
    MatButtonModule],
  templateUrl: './wish.component.html',
  styleUrl: './wish.component.css'
})
export class WishComponent {
  constructor(private authSvc: AuthenticationService,
    public wishtSvc: WishService, 
    public fileHelper: FileHelper,
    private router: Router) {
  }

  showDetail(productId: number) {
    this.router.navigate([`/detail/${productId}`]);
  }

  deleteWish(productId: number) {
    this.wishtSvc.delete(productId, this.authSvc.currentUserValue?.id).subscribe(() => {
      this.wishtSvc.getWishesByUserId(this.authSvc.currentUserValue?.id)
      .subscribe(data => {
        console.log(data)
        this.wishtSvc.wishListValue = data;
      });
    });
  }
}
