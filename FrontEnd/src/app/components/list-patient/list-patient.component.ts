import { Component, inject } from '@angular/core';
import { PatientsService } from '../../shared/services/patients.service';
import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Patient } from '../../shared/models/patient.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-list-patient',
  standalone: true,
  imports: [
    CommonModule,
    HttpClientModule
  ],
  templateUrl: './list-patient.component.html',
  styleUrl: './list-patient.component.scss'
})
export default class ListPatientComponent {

  public service = inject(PatientsService);
  public router = inject(Router);
  public allPatients: Patient[] = [];

  ngOnInit(): void {
    this.loadPatients();
  }

  editPatient(id?: number) {
    if (id == 0) {
      this.router.navigate(['/detail']);
    } else {
      this.router.navigate(['/detail', id]);
    }
  }

  deletePatient(id: number) {
    this.service.deletePatient(id).subscribe((data) => {
      alert("Paciente eliminado correctamente");
      this.loadPatients();
    });
  }

  private loadPatients() {
    this.service.getAllPatients().subscribe((data) => {
      this.allPatients = data;
    });
  }
}
