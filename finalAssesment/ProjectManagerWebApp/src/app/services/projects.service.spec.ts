import { TestBed } from '@angular/core/testing';

import { ProjectsService } from './projects.service';
import { HttpModule } from '@angular/http';

describe('ProjectsService', () => {
  beforeEach(() => TestBed.configureTestingModule({
    imports:[HttpModule]
  }));

  it('should be created', () => {
    const service: ProjectsService = TestBed.get(ProjectsService);
    expect(service).toBeTruthy();
  });
});
