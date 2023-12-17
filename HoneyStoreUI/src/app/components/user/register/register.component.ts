import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatSnackBar, MatSnackBarModule } from '@angular/material/snack-bar';
import { UserService } from '../../../services';
import { Router, RouterModule } from '@angular/router';
import { MustMatch } from '../../../helpers';
import { User } from '../../../models';
import { first } from 'rxjs';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [CommonModule,
    RouterModule,
    FormsModule,
    ReactiveFormsModule,
    MatCardModule,
    MatIconModule,
    MatInputModule,
    MatButtonModule,
    MatSnackBarModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  hide: boolean = true;
  registerForm: FormGroup = new FormGroup({});

  constructor(private userSvc: UserService,
    private formBuilder: FormBuilder,
    private snackBar: MatSnackBar,
    private router: Router) { }

  ngOnInit() {
    this.registerForm = this.formBuilder.group({
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required]],
      confirmPassword: ['', [Validators.required]]
    },
    { validator: MustMatch('password', 'confirmPassword') })
  }

  // convenience getter for easy access to form fields
  get controls() { return this.registerForm.controls; }

  onSubmit() {
    if (this.registerForm.invalid) {
      return;
    }

    const user: User = {
      id: 0,
      firstName: this.controls['firstName'].value,
      lastName: this.controls['lastName'].value,
      email: this.controls['email'].value,
      password: this.controls['password'].value,
      roleName: ''
    }

    this.userSvc.register(user)
      .pipe(first())
      .subscribe(() => {
        this.showNotification('Ваш аккаунт успішно створено.', 'Закрити');
        this.router.navigate(['/login'])
      },
      error => {
        this.showNotification('Помилка! Не вдалося створити новий аккаунт.', 'Закрити')
      });
  }

  showNotification(message: string, action: string) {
    this.snackBar.open(message, action, {
      duration: 5000,
    });
  }
}
