import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { AuthenticationService } from '../../../services';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { first } from 'rxjs';
import { Router, RouterModule } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    MatButtonModule,
    MatCardModule,
    MatIconModule,
    MatInputModule,
    MatSnackBarModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {

  hide: boolean = true;
  loginForm: FormGroup = new FormGroup({});

  constructor(private authService: AuthenticationService,
    private snackBar: MatSnackBar,
    private formBuilder: FormBuilder,
    private router: Router) { }

  ngOnInit() {
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    })
  }

  // convenience getter for easy access to form fields
  get controls() { return this.loginForm?.controls; }

  onSubmit() {
    if (this.loginForm?.invalid) {
      return;
    }

    this.authService.login(this.controls['email'].value, this.controls['password'].value)
      .pipe(first()).subscribe(() => {
        this.router.navigate(['/products']);
      },
      error => {
        this.showNotification('Невдалося ввійти. Пароль або логін введено неправильно.', 'Закрити')
      });
  }

  showNotification(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 5000,
    });
  }
}
