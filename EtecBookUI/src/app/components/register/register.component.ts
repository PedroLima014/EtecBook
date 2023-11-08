import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import ValidateForm from 'src/app/helpers/validateForm';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.css']
})
export class RegisterComponent {

  registerForm!: FormGroup

  constructor (private fb: FormBuilder) {}

  ngOnInit(){
    this.registerForm = this.fb.group({
      name: ['', Validators.required],
      email: ['', Validators.compose([Validators.required, Validators.email])],
      password: ['', Validators.compose([Validators.required, Validators.minLength(6)])],
    });
  }

  onSubmit() {
    if (this.registerForm.valid) {
      // Enviar os dados a API
    } else {
      // Exibir mensagem de erro
      ValidateForm.validadteAllFormFields(this.registerForm);
    }
  }

  get f(){
    return this.registerForm.controls;
  }
}
