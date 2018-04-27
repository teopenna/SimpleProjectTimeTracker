import React, { Component } from 'react';
import ReactDOM from 'react-dom';
import { Link } from 'react-router-dom';
import SimpleProjectTimeTrackerService from '../services/SimpleProjectTimeTrackerService';
import moment from 'moment';

export class TimeRegistrationForm extends Component {
    constructor(props) {
        super(props);
        this.state = {
            timeRegistration: {
                date: '',
                hoursWorked: ''
            },
            projects: [],
            errors: {}
        };

        this.SimpleProjectTimeTrackerService = new SimpleProjectTimeTrackerService('api');

        this.handleInputChange = this.handleInputChange.bind(this);
        this.handleSubmit = this.handleSubmit.bind(this);
    }

    componentDidMount() {
        this.SimpleProjectTimeTrackerService.getProjects()
            .then(res => {
                this.setState({ projects: res.data });
                
                if (this.props.match.path === '/timeregistrations/edit/:id') {
                    this.SimpleProjectTimeTrackerService.getSingleTimeRegistration(this.props.match.params.id)
                        .then(res => {
                            let editTimeRegistration = res.data;
                            editTimeRegistration.date = moment(editTimeRegistration.date).format('YYYY-MM-DD');
                            this.setState({ timeRegistration: editTimeRegistration });
                        });
                } else {
                    let newTimeRegistration = {
                        projectId: this.state.projects[0].id,
                        projectName: '',
                        customerName: '',
                        date: moment().format('YYYY-MM-DD'),
                        hoursWorked: 8
                    }

                    this.setState({ timeRegistration: newTimeRegistration });
                }
            });
    }

    handleSubmit(event) {
        event.preventDefault();
        this.SimpleProjectTimeTrackerService.saveTimeRegistration(this.state.timeRegistration)
            .then(res => {
                if (!res.isError) {
                    this.props.history.push('/timeregistrations');
                }
                else {
                    alert('Error!');
                }
            });
    }

    handleInputChange(event) {
        event.preventDefault();
        const target = event.target;
        
        let value;
        if (target.type.indexOf('select') === 0) {
            value = target.options[target.selectedIndex].value;
        } else {
            value = target.value;
        }
        const name = target.name;
        let timeRegistrationUpdates = {
            [name]: value
        };

        this.setState({
            contact: Object.assign(this.state.timeRegistration, timeRegistrationUpdates)
        });
    }
    
    render() {
        return (
            <fieldset className="form-group">
                <legend>{this.state.timeRegistration.id ? "Edit Time Registration" : "New Time Registration"}</legend>
                <form onSubmit={(e) => this.handleSubmit(e)}>
                    <div>
                        <label htmlFor="selProject" className="form-control-label">Project:</label>
                        <select id="selProject" name="projectId" onChange={(e) => this.handleInputChange(e)} className="form-control form-control-danger" value={this.state.timeRegistration.projectId}>
                            {this.state.projects.map(project =>
                                <option key={project.id} value={project.id}>{project.name} ({project.customerName})</option>
                            )}
                        </select>
                        
                    </div>
                    <div>
                        <label htmlFor="inputDate" className="form-control-label">Date:</label>
                        <input id="inputDate" name="date" onChange={(e) => this.handleInputChange(e)} className="form-control form-control-danger" value={this.state.timeRegistration.date} type="date" />
                    </div>
                    <div>
                        <label htmlFor="inputHoursWorked" className="form-control-label">Hours Worked:</label>
                        <input id="inputHoursWorked" name="hoursWorked" onChange={(e) => this.handleInputChange(e)} className="form-control form-control-danger" value={this.state.timeRegistration.hoursWorked} type="number" />
                    </div>
                    <button className="btn btn-lg btn-primary" type="submit">Save</button>
                    <Link className="btn btn-lg btn-secondary" to="/timeregistrations">Cancel</Link>
                </form>
            </fieldset>
        );
    }
}