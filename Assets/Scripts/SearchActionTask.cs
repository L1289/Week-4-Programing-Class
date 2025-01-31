using NodeCanvas.Framework;
using ParadoxNotion.Design;
using UnityEngine.AI;
using UnityEngine;

namespace NodeCanvas.Tasks.Actions{

	public class SearchActionTask : ActionTask{

		private NavMeshAgent navAgent;
		public Transform playerStartLocation;
		public float range;
		
		

		protected override string OnInit(){
			navAgent = agent.GetComponent<NavMeshAgent>();
			return null;
		}

		protected override void OnExecute(){
			//Choose a random destination on the navmesh
			Vector3 randomLocation = Random.insideUnitSphere * range + agent.transform.position;
			NavMeshHit hit;
			if (!NavMesh.SamplePosition(randomLocation, out hit, range, NavMesh.AllAreas))
			{
				EndAction(false);
				return;
			}

			//Set the path to that position
			navAgent.SetDestination(hit.position);
        }

		protected override void OnUpdate(){
			//When they have arrived then end the task
			if(navAgent.pathPending && navAgent.remainingDistance <= navAgent.stoppingDistance)
			{
				EndAction(true);
			}
		}

		//Called when the task is disabled.
		protected override void OnStop(){
			
		}

		//Called when the task is paused.
		protected override void OnPause(){
			
		}
	}
}