import { Component } from '@angular/core';
import { ApplicationService } from '../../../service/application.service';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import Swal from 'sweetalert2';

@Component({
  selector: 'app-view-applications',
  imports: [CommonModule],
  templateUrl: './view-applications.component.html',
  styleUrl: './view-applications.component.css'
})
export class ViewApplicationsComponent {
  appllications: any[] = [];
imageBaseUrl = 'https://localhost:7089';
statuses: any[]=[];
jobApplicationId:any[]=[];

  constructor(private applicationService: ApplicationService, private router: Router) {} 

  ngOnInit(): void {
    this.getAllApplications();
    this.GetStatuses();

  }
  getAllApplications(): void {
    this.applicationService.getAllApplications().subscribe({
      next: (response) => {
        this.appllications= Array.isArray(response) ? response : (response as any).data;
      },
      error: (error) => console.error('Error fetching application:', error)
    });
  }
   
   isImage(resumePath: string): boolean {
    return resumePath.endsWith('.jpg') || resumePath.endsWith('.jpeg') || resumePath.endsWith('.png') || resumePath.endsWith('.gif');
  }
  GetStatuses()
{
  this.applicationService.GetStatuses().subscribe(
    (statuses)=>{
    this.statuses=statuses;
    console.log('Statuses from API:', statuses);
    },
  )
}
getStatusById(statusName: string): number | null {
debugger;
  const status = this.statuses.find((status) => status.statusName === statusName);
  return status ? status.id : null;
}
UpdateStatus(applications: any, action: string): void {
  debugger;

  console.log('Applications object:', applications);  
  console.log('Available statuses:', this.statuses);
  console.log('Action:', action);

  const statusId = this.getStatusById(action); 

  if (!statusId) {
    Swal.fire('Error!', `Status ID not found for the action: ${action}`, 'error');
    return;
  }

  if (!applications?.id) {
    Swal.fire('Error!', 'Application ID is missing or invalid!', 'error');
    console.log('applications.id:', applications?.id);
    return;
  }

  Swal.fire({
    title: 'Are you sure?',
    text: `You are about to ${action} this application.`,
    icon: 'warning',
    showCancelButton: true,
    confirmButtonText: 'Yes, do it!',
    cancelButtonText: 'Cancel'
  }).then((result) => {
    if (result.isConfirmed) {
      this.applicationService.updateStatus(applications.id, statusId).subscribe(
        () => {
          Swal.fire('Success!', `${action} request status updated successfully.`, 'success');
          this.getAllApplications();
        },
        (error) => {
          Swal.fire('Error!', `${action} request status update failed: ${error.message || error}`, 'error');
        }
      );
    }
  });
}
} 



